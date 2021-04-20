using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCharacteristics
{

    /// <summary>
    /// In AU
    /// </summary>
    public readonly PhysicalValue semimajorAxis;
    /// <summary>
    /// In units
    /// </summary>
    public readonly PhysicalValue eccentricity;
    /// <summary>
    /// In radians
    /// </summary>
    public readonly PhysicalValue inclination;
    /// <summary>
    /// In radians
    /// </summary>
    public readonly PhysicalValue longitudeOfTheAscendingNode;
    /// <summary>
    /// In radians
    /// </summary>
    public readonly PhysicalValue argumentOfPeriapsis;
    /// <summary>
    /// In radians at epoch t0
    /// </summary>
    public readonly PhysicalValue meanAnomalyAtT0;

    public OrbitalCharacteristics
        (PhysicalValue semimajorAxis, PhysicalValue eccentricity, PhysicalValue inclination, PhysicalValue longitudeOfTheAscendingNode, PhysicalValue argumentOfPeriapsis, PhysicalValue meanAnomalyAtT0)
    {
        this.semimajorAxis = semimajorAxis;
        this.eccentricity = eccentricity;
        this.inclination = inclination;
        this.longitudeOfTheAscendingNode = longitudeOfTheAscendingNode;
        this.argumentOfPeriapsis = argumentOfPeriapsis;
        this.meanAnomalyAtT0 = meanAnomalyAtT0;
    }

    public (Vector3,Vector3) CalculateStartPositionAndVelocity(AstronomicalObject centralBody, double timeOffset)
    {
        //[m^3/s^2]
        PhysicalValue standardGravitationalParameter = centralBody.Mass * StellarConstants.GravitationalConstant;

        //[rad]
        PhysicalValue meanAnomaly = CalculateCurrentMeanAnomaly(standardGravitationalParameter, timeOffset);
        //[rad]
        PhysicalValue eccentricAnomaly = CalculateEccentricAnomaly(meanAnomaly);
        //[rad]
        PhysicalValue trueAnomaly = CalculateTrueAnomaly(eccentricAnomaly);

        //[m]=[m]*[1]
        PhysicalValue distanceToCentralBody = semimajorAxis*(1 - eccentricity * Math.Cos((double)eccentricAnomaly));

        //[m,m,m]=[m]*[1,1,1]
        Vector3 positionInOrbitalFrame = (float)distanceToCentralBody * new Vector3((float)Math.Cos((double)trueAnomaly), (float)Math.Sin((double)trueAnomaly));


        //[m/s]=sqrt([m^3/s^2]*[m])/[m]
        Vector3 velocityInOrbitalFrame = (float)(PhysicalValue.Sqrt(standardGravitationalParameter * semimajorAxis) / (distanceToCentralBody)) *
         new Vector3((float)-Math.Sin((double)eccentricAnomaly), (float)(Math.Sqrt(1 - Math.Pow((double)eccentricity, 2)) * Math.Cos((double)eccentricAnomaly)));



        (double[] positionCoordinates, double[] velocityCoordinates) = ConvertFromOrbitalFrameToHeliocentricFrame(positionInOrbitalFrame, velocityInOrbitalFrame);

        return (VectorFromDoubleArray(positionCoordinates), VectorFromDoubleArray(velocityCoordinates));
    }
    /// <summary>
    /// Also converts vertical axis from z to y!
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    private Vector3 VectorFromDoubleArray(double[] array)
    {
        return new Vector3((float)array[0], (float)array[1], (float)array[2]);
    }

    private (double[],double[]) ConvertFromOrbitalFrameToHeliocentricFrame(Vector3 positionInOrbitalFrame, Vector3 velocityInOrbitalFrame)
    {
        double aOP = (double)argumentOfPeriapsis;
        double lAN = (double)longitudeOfTheAscendingNode;
        double i = (double)inclination;

        double rotCoeff1 = (Math.Cos(aOP) * Math.Cos(lAN) - Math.Sin(aOP) * Math.Cos(i) * Math.Sin(lAN));
        double rotCoeff2 = (Math.Sin(aOP) * Math.Cos(lAN) + Math.Cos(aOP) * Math.Cos(i) * Math.Sin(lAN));

        double x = positionInOrbitalFrame.x * rotCoeff1 - positionInOrbitalFrame.y * rotCoeff2;
        double vx = velocityInOrbitalFrame.x * rotCoeff1 - velocityInOrbitalFrame.y * rotCoeff2;

        double rotCoeff3 = (Math.Cos(aOP) * Math.Sin(lAN) + Math.Sin(aOP) * Math.Cos(i) * Math.Cos(lAN));
        double rotCoeff4 = (Math.Cos(aOP) * Math.Cos(i) * Math.Cos(lAN) - Math.Sin(aOP) * Math.Sin(lAN));

        double y = positionInOrbitalFrame.x * rotCoeff3 + positionInOrbitalFrame.y * rotCoeff4;
        double vy = velocityInOrbitalFrame.x * rotCoeff3 + velocityInOrbitalFrame.y * rotCoeff4;

        double rotCoeff5 = (Math.Sin(aOP) * Math.Sin(i));
        double rotCoeff6 = (Math.Cos(aOP) * Math.Sin(i));

        double z = positionInOrbitalFrame.x *rotCoeff5  + positionInOrbitalFrame.y * rotCoeff6;
        double vz = velocityInOrbitalFrame.x * rotCoeff5 + velocityInOrbitalFrame.y * rotCoeff6;

        return (new double[3] { x, y, z }, new double[3] { vx, vy, vz });
    }

    private PhysicalValue CalculateTrueAnomaly(PhysicalValue eccentricAnomaly)
    {

        PhysicalValue y = PhysicalValue.Sqrt(1 + eccentricity) * Math.Sin((double)eccentricAnomaly / 2);
        PhysicalValue x = PhysicalValue.Sqrt(1 - eccentricity) * Math.Cos((double)eccentricAnomaly / 2);
        PhysicalValue trueAnomaly = 2 * Math.Atan2((double)y, (double)x);
        return trueAnomaly;
    }

    private PhysicalValue CalculateEccentricAnomaly(PhysicalValue meanAnomaly)
    {
        Func<double, double> KeplersEquation = x => x - (double)eccentricity * Math.Sin(x) - (double)meanAnomaly;
        Func<double, double> KeplersEqationDerivative = x => 1 - (double)eccentricity * Math.Cos(x);
        double eccentricAnomaly = NewtonRaphsonMethod.newtonRaphson((double)meanAnomaly, KeplersEquation, KeplersEqationDerivative);
        return eccentricAnomaly;
    }

    private PhysicalValue CalculateCurrentMeanAnomaly(PhysicalValue standardGravitationalParameter, double timeOffset)
    {
        if (GlobalSettings.startTimeInJulianDays == StellarConstants.EpochJ2000&&timeOffset==0) return meanAnomalyAtT0;
        PhysicalValue deltaT=0;
        if (timeOffset != 0) deltaT = 86400 * (GlobalSettings.startTimeInJulianDays - timeOffset);
        if(timeOffset == 0)   deltaT = 86400 * (GlobalSettings.startTimeInJulianDays - StellarConstants.EpochJ2000);
        //[rad]=[rad]+[s]*[sqrt([m^3/s^2]/[m^3])]
        PhysicalValue retVal = PhysicalValue.ClampRadians(meanAnomalyAtT0 + deltaT * PhysicalValue.Sqrt(standardGravitationalParameter / PhysicalValue.Pow(semimajorAxis, 3)));
        return retVal;
    }

}
