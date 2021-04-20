using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;


    private float lerpTime = 1f;
    private float currentLerpTime;

    private int scrollMagnitude = 10;
    private float scrollMin = 10;
    private float scrollMax = 1000;


    private float targetOrtographicSize;

    private float dampSmoothTime = 0.2f;
    private float currentMoveLerpTime;
    private Vector3 targetCameraPosition;
    private Vector3 cameraMoveVelocity;

    private float moveMagnitude = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        targetOrtographicSize = Camera.main.orthographicSize;
        targetCameraPosition = Camera.main.transform.position;

        cameraController = this;
    }

    // Update is called once per frame
    void Update()
    {
        ScrollCamera();

        MoveCamera();
    }

    private void MoveCamera()
    {
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, targetCameraPosition, ref cameraMoveVelocity, dampSmoothTime);
    }

    private void ScrollCamera()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float percScroll = currentLerpTime / lerpTime;
        Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, targetOrtographicSize, percScroll);
    }

    public void ChangerOrtographicCameraSize(float delta)
    {
        float trueDelta = delta * scrollMagnitude * Mathf.Exp(targetOrtographicSize * 0.005f);

        if (targetOrtographicSize- trueDelta <= scrollMin&&delta>0) return;

        if (targetOrtographicSize - trueDelta >= scrollMax && delta < 0) return;
        currentLerpTime = 0f;
        targetOrtographicSize -= trueDelta;
    }


    public void MoveCamera(Vector3 direction)
    {
        targetCameraPosition += direction*moveMagnitude*Mathf.Exp(targetOrtographicSize*0.005f);
    }

    public void SetCameraTarget(Vector3 target)
    {
        targetCameraPosition = new Vector3 (target.x,target.y,targetCameraPosition.z);
    }
}
