using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarComponent : MonoBehaviour
{
    public float colorBV;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        Color starColor = ColorConverter.ConvertBVToRGB(colorBV);
        mr.material.color = starColor;
        mr.material.EnableKeyword("_EMISSION");
        mr.material.SetColor("_EmissionColor", starColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
