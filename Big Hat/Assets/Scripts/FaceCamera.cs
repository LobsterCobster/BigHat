using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FaceCamera : MonoBehaviour
{
    public Camera cam;
    public Quaternion Origin;
    // Start is called before the first frame update
    void Start()
    {
        Origin = this.transform.rotation;
        cam = GameObject.Find("Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.enabled)
        {
            this.transform.LookAt(cam.transform.position);
        }
        else
        {
            this.transform.rotation = Origin;
        }
    }
}
