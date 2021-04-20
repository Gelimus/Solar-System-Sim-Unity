using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetarySystem : MonoBehaviour
{
    public static PlanetarySystem main;
    public List<AstronomicalObject> SystemElements { get; private set; } = new List<AstronomicalObject>();
    public AstronomicalObject CentralBody { get; private set; }
    Vector3 Barycenter;
    private void Awake()
    {
        SystemElements.AddRange(FindObjectsOfType<AstronomicalObject>());
        foreach (AstronomicalObject ao in SystemElements)
        {
            if (ao.TryGetComponent<StarComponent>(out _))
            {
                CentralBody = ao;
            }
        }
        main = this;
    }
    void Start()
    {

        calculateProperStartPositions();
        calculateSystemBarycenter();

        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaT = Time.deltaTime* GlobalSettings.timeScale;
        foreach(AstronomicalObject ao in SystemElements)
        {
            AccelerateAstronomicalObject(ao,deltaT);
            
        }
    }


    public void resizeBodies()
    {
        foreach(AstronomicalObject ao in SystemElements)
        {
            ao.SetProperGameObjectSize();
        }
    }
    private void calculateProperStartPositions()
    {
        foreach(AstronomicalObject ao in SystemElements)
        {
            if (ao == CentralBody)
            {
                ao.transform.position = Vector3.zero;
                continue;
            }
            if (ao.name == "Luna")
            {
                ao.SetStartingPositionAndVelocity(ao.transform.parent.GetComponent<AstronomicalObject>());
                continue;
            }

            ao.SetStartingPositionAndVelocity(CentralBody);
        }
    }

    private void calculateSystemBarycenter()
    {
        PhysicalValue totalMass=PhysicalValue.Zero;
        foreach (AstronomicalObject ao in SystemElements)
        {
            Barycenter += ao.transform.position * (float)ao.Mass;
            totalMass += ao.Mass;
        }
        Barycenter /= (float)totalMass;


    }
    private void AccelerateAstronomicalObject(AstronomicalObject anchorObject, float deltaT)
    {
        //[[m/s^2]
        Vector3 acceleration = CalculateTotalGravitationlAcceleration(anchorObject);
        //Debug.Log(acceleration.magnitude);
        //[m/s]
        Vector3 deltaV = acceleration * deltaT;
        anchorObject.Accelerate(deltaV);
    }
    private Vector3 CalculateTotalGravitationlAcceleration(AstronomicalObject anchorObject)
    {
        Vector3 resultantGravitationalForce = Vector3.zero;
        foreach(AstronomicalObject ao in SystemElements)
        {
            if (ao == anchorObject) continue;

            resultantGravitationalForce+= CalculateGravitationalAcceleration(anchorObject, ao);
        }

        return resultantGravitationalForce;
    }

    private Vector3 CalculateGravitationalAcceleration(AstronomicalObject astronomicalObject1, AstronomicalObject astronomicalObject2)
    {
        //[m^3/s^2]=[(m^3)/(kg*s^2)]*[kg]
        PhysicalValue massProd = StellarConstants.GravitationalConstant* astronomicalObject2.Mass;
        //[AU]
        Vector3 r12 = (astronomicalObject1.transform.position - astronomicalObject2.transform.position) ;
        //[m/s^2]=[m^3/s^2]*[(AU*m/AU)^2]
        Vector3 gravitationalForce = ((float)((double)-massProd / Math.Pow(r12.magnitude*GlobalSettings.distanceScale,2))) * r12.normalized;
        //if (astronomicalObject1.name == "Luna" && astronomicalObject2.name == "Earth") Debug.Log(gravitationalForce.magnitude);
        return gravitationalForce;
    }
}
