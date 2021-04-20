using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronomicalObject : MonoBehaviour
{
    

    new public string name;

    private PhysicalValue _mass;
    /// <summary>
    /// Mass in kg
    /// </summary>
    public PhysicalValue Mass {
        set
        {
            _mass = value;
        }
        get
        {
            return _mass * massMultiplier;
        }
    }
    private double massMultiplier=1;
    /// <summary>
    /// Density in kg/m^3
    /// </summary>
    public PhysicalValue averageDensity;
    /// <summary>
    /// Volume expressed in m^3;
    /// </summary>
    PhysicalValue volume;
    /// <summary>
    /// Radius expressed in m
    /// </summary>
    PhysicalValue radius;
    //TODO: JUST TEMPORARY VARIABLES!!!
    //public PhysicalValue SEMIMAJORAXIS;
    //public PhysicalValue ECCENTRICITY;
    //public PhysicalValue INCLINATION;
    //public PhysicalValue LONGITUDEOFTHEASCENDINGNODE;
    //public PhysicalValue ARGUMENTOFPERIAPSIS;
    //public PhysicalValue MEANANOMALY;
    [HideInInspector]
    public double timeOffset = 0;
    public OrbitalCharacteristics orbit;

    private Vector3 currentVelocity = Vector3.zero;

    // Start is called before the first frame update
    void Awake()
    {
        switch (name)
        {
            case "Sun":
                assignValues(TMPValues.Sun);
                break;
            case "Earth":
                assignValues(TMPValues.Earth);
                break;
            case "Luna":
                assignValues(TMPValues.Luna);
                break;
            case "Mercury":
                assignValues(TMPValues.Mercury);
                break;
            case "Venus":
                assignValues(TMPValues.Venus);
                break;
            case "Mars":
                assignValues(TMPValues.Mars);
                break;
            case "Jupiter":
                assignValues(TMPValues.Jupiter);
                break;
            case "Saturn":
                assignValues(TMPValues.Saturn);
                break;
            case "Uranus":
                assignValues(TMPValues.Uranus);
                break;
            case "Neptune":
                assignValues(TMPValues.Neptune);
                break;
            case "Pluto":
                assignValues(TMPValues.Pluto);
                break;
            case "HalleysComet":
                assignValues(TMPValues.HalleysComet);
                break;
            case "Ceres":
                assignValues(TMPValues.Ceres);
                break;
            case "Eris":
                assignValues(TMPValues.Eris);
                break;
        }
        CalcVolume();
        CalcRadius();
        SetProperGameObjectSize();
       // DebugCharacteristics();
    }

    private void assignValues(AstronomicalObject other)
    {
        Mass = other.Mass;
        averageDensity = other.averageDensity;
        timeOffset = other.timeOffset;
        orbit = other.orbit;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        //if (name == "Earth")  Debug.Log($"Current velocity is {currentVelocity.x}, {currentVelocity.y}, {currentVelocity.z}");
        transform.position += currentVelocity * Time.deltaTime*GlobalSettings.timeScale;
    }

    public void DebugCharacteristics()
    {
        Debug.Log(name + "'s mass equals " + Mass/StellarConstants.EarthMass + " x Earth mass, which equals " + Mass  + " " + StellarConstants.MassUnit);
        Debug.Log(name + "'s average density equals " + averageDensity/StellarConstants.EarthDensity + " x Earth density, which equals " + averageDensity  + " " + StellarConstants.DensityUnit);
        Debug.Log(name + "'s volume equals " + volume/StellarConstants.EarthVolume + " x Earth volume, which equals " + radius + " " + StellarConstants.VolumeUnit);
        Debug.Log(name + "'s radius equals " + radius/StellarConstants.EarthMeanRadius + " x Earth radius, which equals " + radius + " " + StellarConstants.RadiusUnit);
    }

    public void Accelerate(Vector3 deltaV)
    {
        currentVelocity += deltaV/GlobalSettings.distanceScale;
    }
    
    public void SetStartingPositionAndVelocity(AstronomicalObject centralBody)
    {
       
        (Vector3 position, Vector3 velocity) = orbit.CalculateStartPositionAndVelocity(centralBody, timeOffset);
        transform.localPosition = position /GlobalSettings.distanceScale;
        currentVelocity = velocity / GlobalSettings.distanceScale + centralBody.currentVelocity;
    }


    private void CalcVolume()
    {
        volume=Mass/averageDensity;
    }
    private void CalcRadius()
    {
        radius = PhysicalValue.Root((double)(3 * volume / (4 * Math.PI)),3);
    }

    public void SetProperGameObjectSize()
    {
        if (name == "Sun")
        {
            transform.localScale = Vector3.one * (float)(radius/GlobalSettings.distanceScale) * GlobalSettings.starScale;
            return;
        }
        transform.localScale = Vector3.one* (float)(radius/GlobalSettings.distanceScale) *GlobalSettings.bodiesSizeScale;
    }

    public void LightUp(bool s)
    {
        if (TryGetComponent<StarComponent>(out _)) return;
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if (!s)
        {
            mr.material.DisableKeyword("_EMISSION");
            return;
        }

        mr.material.EnableKeyword("_EMISSION");
        mr.material.SetColor("_EmissionColor", Color.magenta);

    }

    public void SetMassMultiplier(double massMult)
    {
        massMultiplier = massMult;
        Debug.Log(massMultiplier);
    }

}
