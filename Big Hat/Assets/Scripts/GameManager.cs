using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public Canvas canvas;
    public Canvas cameraCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.enabled)
        {
            canvas.enabled = false;
            cameraCanvas.enabled = true;
        }
        else
        {
            canvas.enabled = true;
            cameraCanvas.enabled = false;
        }
    }
}
