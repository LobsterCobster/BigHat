using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
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
    public void Capture()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.BoxCast(cam.transform.position, cam.transform.localScale, cam.transform.forward, out hit, cam.transform.rotation, 10))
        {
            if (hit.transform.CompareTag("Capture"))
            {
                ScreenCapture.CaptureScreenshot(hit.transform.name + " " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
                Debug.Log("A screenshot of object taken!");

            }
            else
            {
                ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
                Debug.Log("A screenshot was taken!");
            }
        }
        else
        {
            ScreenCapture.CaptureScreenshot("screenshot " + System.DateTime.Now.ToString("MM-dd-yy (HH-mm-ss)") + ".png");
            Debug.Log("A screenshot was taken!");
        }
    }
}
