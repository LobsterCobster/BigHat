using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    public List<PhotoObject> photos = new List<PhotoObject>();
    private Camera cam;
    public Text camtext;
    private bool timer;

    private float time;
    
    public List<string> images = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                camtext.text = "";
                timer = false;
            }
        }
    }
    public void Capture()
    {
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.transform.CompareTag("Capture"))
            {
                bool captured = false;
                foreach (var item in photos)
                {
                    if (item.Object.name == hit.transform.name && item.isCaptured == true)
                    {
                        captured = true; 
                        break;
                    }
                }
                if (!captured)
                {
                    int objectid = GetObjectId(hit.transform.name);
                    StartCoroutine(CaptureScreen(objectid));
                    DisplayText(hit.transform.name);
                }
            }
        }
    }
    public void DisplayText(string text)
    {
        camtext.text = text;
        timer = true;
        time = 0f;
    }
    public int GetObjectId(string text)
    {
        int id = 0;
        images.Add(text);
        foreach (PhotoObject a in photos)
        {
            if (text == a.Object.name)
            {
                a.isCaptured = true;
            }
        }
        id = images.Count;
        return id;
    }
    public IEnumerator CaptureScreen(int objectid)
    {
        yield return null;
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = false;

        yield return new WaitForEndOfFrame();

        ScreenCapture.CaptureScreenshot(objectid + ".png");

        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = true;
    }
    [System.Serializable]
    public class PhotoObject
    {
        public GameObject Object;
        public bool isCaptured;
    }
}
