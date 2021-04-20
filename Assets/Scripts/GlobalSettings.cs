using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings 
{
    public const float timeScaleMin = 86400;

    public const float timeScaleMax = 86663520;

    public static float timeScale = 218400;

    public static float startTimeInJulianDays= 2451545;

    public const float bodiesScaleMin = 1;

    public const float bodiesScaleMax = 10000;

    public static float bodiesSizeScale = 1;

    public const float starScaleMin = 1;

    public const float starScaleMax = 1000;

    public static float starScale = 1;

    public static readonly float distanceScale = ((float)StellarConstants.AU)/20f;
}
