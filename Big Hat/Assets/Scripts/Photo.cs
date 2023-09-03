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
    public bool timer;

    private float time;
    
    public List<string> images = new List<string>();

    Texture2D currentCapture;

    public List<Sprite> sprites = new List<Sprite>();

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
        Debug.Log(text);
        camtext.color = new Color(255, 255, 255, 255);
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
    public string GetName(int Id)
    {
        string objectName = photos[Id].Object.name;
        return objectName;
    }
    public IEnumerator CaptureScreen(int objectid)
    {
        yield return null;
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = false;

        yield return new WaitForEndOfFrame();

        currentCapture = new Texture2D(Screen.width,Screen.height, TextureFormat.RGB24, false);
        currentCapture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        currentCapture.Apply();
        Sprite sprite = Sprite.Create(currentCapture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
        sprites.Add(sprite);


        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = true;
    }
    [System.Serializable]
    public class PhotoObject
    {
        public GameObject Object;
        public bool isCaptured;
    }
}
