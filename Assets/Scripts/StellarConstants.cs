using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StellarConstants
{
    public static readonly PhysicalValue EarthMass = new PhysicalValue(5.9722m, 24);

    public static readonly string MassUnit = "kg";

    public static readonly PhysicalValue EarthDensity = new PhysicalValue(5.514m,3);

    public static readonly string DensityUnit = "kg/m3";

    public static readonly PhysicalValue EarthVolume = new PhysicalValue(1.08321m, 15);

    public static readonly string VolumeUnit = "m^3";

    public static readonly PhysicalValue EarthMeanRadius = new PhysicalValue(6.371m, 6);

    public static readonly string RadiusUnit = "m";
    //6.67430×10−11  (m^3)/(kg*s^2)
    public static readonly PhysicalValue GravitationalConstant = new PhysicalValue(6.67430m, -11);
    //In Julian Days
    public static readonly double EpochJ2000 = 2451545;
    //In seconds
    public static readonly double JulianDay = 86400; //s

    public static readonly PhysicalValue AU = new PhysicalValue(1.495978707m, 11); //m


    public static DateTime ConvertToGregorian(double julianDays)
    {
        int f = (int)julianDays + 68569;


        int e = (4 * f) / 146097;

        int g = f - (146097 * e + 3) / 4;

        int h = 4000 * (g + 1) / 1461001;

        int t = g - (1461 * h / 4) + 31;

        int u = (80 * t) / 2447;

        int v = u / 11;

        // Determine year, month, and day for corresponding calendar date

        int year = 100 * (e - 49) + h + v;

        int mon = u + 2 - 12 * v;

        int day = t - 2447 * u / 80;

        return new DateTime(year, mon, day);
    }
}
