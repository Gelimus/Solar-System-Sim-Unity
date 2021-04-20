using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorConverter
{

    public static Color ConvertBVToRGB(float bv)
    {
        float t = ConvertBVToKelvin(bv);

        float x, y;
        (x,y)=ConvertKelvinToxyY(t);

        float X, Y, Z;
        (X,Y,Z) = ConvertxyYtoXYZ(x, y);
        float R, G, B;
        (R,G,B) = ConvertXYZyoRGB(Y, X, Z);

        return new Color(R, G, B);
    }

    private static (float, float, float) ConvertXYZyoRGB(float Y, float X, float Z)
    {
        float r = 3.2406f * X - 1.5372f * Y - 0.4986f * Z;
        float g = -0.9689f * X + 1.8758f * Y + 0.0415f * Z;
        float b = 0.0557f * X - 0.2040f * Y + 1.0570f * Z;
        return (r, g, b);
    }

    private static (float,float,float) ConvertxyYtoXYZ(float x, float y)
    {
        float Y = (y == 0) ? 0 : 1;
        float X = (y == 0) ? 0 : (x * Y) / y;
        float Z = (y == 0) ? 0 : ((1 - x - y) * Y) / y;
        return (X, Y, Z);
    }

    private static (float,float) ConvertKelvinToxyY(float t)
    {
        float x = 0;
        float y = 0;
        if (t >= 1667 & t <= 4000)
        {
            x = (-0.2661239f * Mathf.Pow(10, 9) / Mathf.Pow(t, 3)) + ((-0.2343580f * Mathf.Pow(10, 6)) / Mathf.Pow(t, 2)) + ((0.8776956f * Mathf.Pow(10, 3)) / t) + 0.179910f;
        }
        if (t > 4000 & t <= 25000)
        {
            x = ((-3.0258469f * Mathf.Pow(10, 9)) / Mathf.Pow(t, 3)) + ((2.1070379f * Mathf.Pow(10, 6)) / Mathf.Pow(t, 2)) + ((0.2226347f * Mathf.Pow(10, 3)) / t) + 0.240390f;
        }

        if (t >= 1667 & t <= 2222)
        {
            y = -1.1063814f * Mathf.Pow(x, 3) - 1.34811020f * Mathf.Pow(x, 2) + 2.18555832f * x - 0.20219683f;
        }
        if (t > 2222 & t <= 4000)
        {
            y = -0.9549476f * Mathf.Pow(x, 3) - 1.37418593f * Mathf.Pow(x, 2) + 2.09137015f * x - 0.16748867f;
        }
        if (t > 4000 & t <= 25000)
        {
            y = 3.0817580f * Mathf.Pow(x, 3) - 5.87338670f * Mathf.Pow(x, 2) + 3.75112997f * x - 0.37001483f;
        }
        return (x, y);
    }

    private static float ConvertBVToKelvin(float bv)
    {
        return 4600 * ((1 / ((0.92f * bv) + 1.7f)) + (1 / ((0.92f * bv) + 0.62f)));
    }

}

