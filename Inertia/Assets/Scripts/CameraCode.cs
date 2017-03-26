using System.Collections;

using System.Collections.Generic;

using UnityEngine;


public class CameraCode : MonoBehaviour
{
    public Camera camera;
    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float yAxisValue = Input.GetAxis("Vertical");
        if (camera != null)
        {
            camera.transform.Translate(new Vector3(xAxisValue, yAxisValue, 0.0f));
        }

    }
}
