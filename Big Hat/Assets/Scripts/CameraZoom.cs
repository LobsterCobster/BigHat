using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    public GameObject CamObject;
    private Camera cam;
    private Vector3 origin;
    public float zoomAMT = 60f;
    // Start is called before the first frame update
    void Start()
    {
        cam = CamObject.GetComponent<Camera>();
        origin = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = zoomAMT;
    }
    public void Zoom(float zoom)
    {
        zoomAMT = zoom;
    }
}
