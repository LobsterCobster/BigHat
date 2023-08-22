using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Change()
    {
        if (cam.enabled == false)
        {
            cam.enabled = true;
        }
        else
        {
            cam.enabled = false;
        }
    }
}
