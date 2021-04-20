using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMouseController : MonoBehaviour
{
    private float scrollSensitivityThreshold = 0.0001f;

    private CameraController cameraControllerReference;
    // Start is called before the first frame update
    void Start()
    {
        cameraControllerReference = GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseScroll = Input.mouseScrollDelta.y;
        
        if(mouseScroll>= scrollSensitivityThreshold|| mouseScroll <= -scrollSensitivityThreshold)
        {
            cameraControllerReference.ChangerOrtographicCameraSize(mouseScroll);
        }


        if (Input.GetKey(KeyCode.W))
        {
            cameraControllerReference.MoveCamera(Vector2.up);
        }

        if (Input.GetKey(KeyCode.A))
        {
            cameraControllerReference.MoveCamera(Vector2.left);
        }

        if (Input.GetKey(KeyCode.S))
        {
            cameraControllerReference.MoveCamera(Vector2.down);
        }

        if (Input.GetKey(KeyCode.D))
        {
            cameraControllerReference.MoveCamera(Vector2.right);
        }
    }
}
