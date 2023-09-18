using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateIsoView : MonoBehaviour
{
    public Camera cam;
    private float Speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cam.enabled)
        {
            if (SystemInfo.deviceType == DeviceType.Desktop)
            {
                if (Input.GetMouseButton(1))
                {
                    transform.eulerAngles += Speed * new Vector3(0, Input.GetAxis("Mouse X"), 0);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = false;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }
}
