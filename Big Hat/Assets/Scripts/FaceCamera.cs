using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class FaceCamera : MonoBehaviour
{
    public GameObject cam;
    public Quaternion Origin;
    // Start is called before the first frame update
    void Start()
    {
        Origin = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.GetComponent<Camera>().enabled)
        {
            this.transform.LookAt(cam.transform.position);
        }
        else
        {
            this.transform.rotation = Origin;
        }
    }
}
