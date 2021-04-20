using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TMPValues 
{
    public static AstronomicalObject Sun = new AstronomicalObject()
    {
        Mass = new PhysicalValue(1.9885m, 30),
        averageDensity = new PhysicalValue(1.408m, 3)
        
    };

    public static AstronomicalObject Mercury = new AstronomicalObject()
    {
        Mass = new PhysicalValue(3.3011m, 23),
        averageDensity = new PhysicalValue(5.427m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(5.7909m, 10), new PhysicalValue(2.0563m, -1), new PhysicalValue(5.8992128717m, -2),
            new PhysicalValue(8.4353508078m, -1), new PhysicalValue(5.0830969135m, -1), new PhysicalValue(3.0507657193m, 0))
    };

    public static AstronomicalObject Venus = new AstronomicalObject()
    {
        Mass = new PhysicalValue(4.8675m, 24),
        averageDensity = new PhysicalValue(5.243m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(1.08209m, 11), new PhysicalValue(6.772m, -3), new PhysicalValue(6.7369709127m, -2),
            new PhysicalValue(1.3383184704m, -1), new PhysicalValue(9.5790650666m, -1), new PhysicalValue(8.7467175464m, -1))
    };


    public static AstronomicalObject Earth = new AstronomicalObject()
    {
        Mass = new PhysicalValue(5.9724m, 24),
        averageDensity = new PhysicalValue(5.514m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(1.49596m, 11), new PhysicalValue(1.671022m, -2), new PhysicalValue(1.2487830798m, -1),
            new PhysicalValue(-1.9653524388m, -1), new PhysicalValue(1.9933026651m, 0), new PhysicalValue(6.2590474036m, 0))
    };
    public static AstronomicalObject Luna = new AstronomicalObject()
    {
        Mass = new PhysicalValue(7.346m, 22),
        averageDensity = new PhysicalValue(3.344m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(3.844m, 8), new PhysicalValue(5.49m, -2), new PhysicalValue(8.9797190015m, -2),
            new PhysicalValue(2.1830578284m, 0), new PhysicalValue(5.5527650152m, 0), new PhysicalValue(2.3609068792m, 0))
    };

    public static AstronomicalObject Mars = new AstronomicalObject()
    {
        Mass = new PhysicalValue(6.4171m, 23),
        averageDensity = new PhysicalValue(3.933m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(2.27923m, 11), new PhysicalValue(9.35m, -2), new PhysicalValue(9.8611102738m, -2),
        new PhysicalValue(8.649502707m, -1), new PhysicalValue(5.0004032135m, 0), new PhysicalValue(3.388033144m, -1))
    };

    public static AstronomicalObject Jupiter = new AstronomicalObject()
    {
        Mass = new PhysicalValue(1.89819m, 27),
        averageDensity = new PhysicalValue(1.326m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(7.78570m, 11), new PhysicalValue(4.89m, -2), new PhysicalValue(1.0629055145m, -1),
        new PhysicalValue(1.7534275797m, 0), new PhysicalValue(4.7798808626m, 0), new PhysicalValue(3.4941491625m, -1))
    };
    public static AstronomicalObject Saturn = new AstronomicalObject()
    {
        Mass = new PhysicalValue(5.6834m, 26),
        averageDensity = new PhysicalValue(6.87m, 2),
        orbit = new OrbitalCharacteristics(new PhysicalValue(1.433529m, 12), new PhysicalValue(5.65m, -2), new PhysicalValue(9.6167641785m, -2),
        new PhysicalValue(1.9838284943m, 0), new PhysicalValue(5.9235078549m, 0), new PhysicalValue(5.5330427947m, 0))
    };

    public static AstronomicalObject Uranus = new AstronomicalObject()
    {
        Mass = new PhysicalValue(8.6813m, 25),
        averageDensity = new PhysicalValue(1.271m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(2.872463m, 12), new PhysicalValue(4.57m, -2), new PhysicalValue(1.1309733553m, -1),
    new PhysicalValue(1.2916483662m, 0), new PhysicalValue(1.6929494253m, 0), new PhysicalValue(2.4825318934m, 0))
    };

    public static AstronomicalObject Neptune = new AstronomicalObject()
    {
        Mass = new PhysicalValue(1.02413m, 26),
        averageDensity = new PhysicalValue(1.638m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(4.49506m, 12), new PhysicalValue(1.1m, -2), new PhysicalValue(1.122246709m, -1),
    new PhysicalValue(2.3000647014m, 0), new PhysicalValue(4.8229730418m, 0), new PhysicalValue(4.4720222358m, 0))
    };

    public static AstronomicalObject Pluto = new AstronomicalObject()
    {
        Mass = new PhysicalValue(1.303m, 22),
        averageDensity = new PhysicalValue(1.854m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(5.869656m, 12), new PhysicalValue(2.444m, -1), new PhysicalValue(2.0734511514m, -1),
    new PhysicalValue(1.9250807117m, 0), new PhysicalValue(1.9867781007m, 0), new PhysicalValue(2.5359634031m, -1))
    };

    public static AstronomicalObject HalleysComet = new AstronomicalObject()
    {
        Mass = new PhysicalValue(2.2m, 14),
        averageDensity = new PhysicalValue(6m, 2),
        timeOffset = 2449400.5,
        orbit = new OrbitalCharacteristics(new PhysicalValue(2.667932034m, 12), new PhysicalValue(9.6714m, -1), new PhysicalValue(2.8319712443m, 0),
    new PhysicalValue(1.019621349m, 0), new PhysicalValue(1.9430750562m, 0), new PhysicalValue(6.6985736692m, -1))
    };
    
    public static AstronomicalObject Ceres = new AstronomicalObject()
    {
        Mass = new PhysicalValue(9.39300m, 20),
        averageDensity = new PhysicalValue(2.162m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(4.138016m, 11), new PhysicalValue(7.816842657453259m, -2), new PhysicalValue(1.8479370797m, -1),
    new PhysicalValue(1.401016952m, 0), new PhysicalValue(1.2867420685m, 0), new PhysicalValue(3.5874442621m, 0))
    };

    public static AstronomicalObject Eris = new AstronomicalObject()
    {
        Mass = new PhysicalValue(1.6466m, 22),
        averageDensity = new PhysicalValue(2.430m, 3),
        orbit = new OrbitalCharacteristics(new PhysicalValue(1.016316951m, 13), new PhysicalValue(4.346564120804066m, -1), new PhysicalValue(7.6746254675m, -1),
    new PhysicalValue(6.2796934513m, -1), new PhysicalValue(2.6451248961m, 0), new PhysicalValue(3.6025822907m, 0))
    };
}
