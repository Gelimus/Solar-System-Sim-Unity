using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonRaphsonMethod
{
    private static double EPSILON = 0.0001;

    public static double newtonRaphson(double x, Func<double,double> func, Func<double,double> derivFunc)
    {
        double h = func(x) / derivFunc(x);
        while (Math.Abs(h) >= EPSILON)
        {
            h = func(x) / derivFunc(x);

            // x(i+1) = x(i) - f(x) / f'(x) 
            x = x - h;
        }
        return x;
    }
}
