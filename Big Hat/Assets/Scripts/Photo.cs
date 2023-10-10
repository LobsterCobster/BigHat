using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    private Database database;
    private Camera cam;
    public Text camtext;
    public bool timer;

    private float time;
    Texture2D currentCapture;

    public Scrapbook scrapbook;

    public List<Sprite> sprites;

    public LayerMask capture;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        database = GameObject.Find("DatabaseInstance").GetComponent<CreateDatabase>().database;
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
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, capture))
        {
            if (hit.transform.root.CompareTag("Capture"))
            {
                bool captured = false;
                string name = hit.transform.root.name;
                foreach (var item in database.organisms)
                {
                    if (item.name == name && item.isCaptured == true)
                    {
                        captured = true; 
                        break;
                    }
                }
                if (!captured)
                {
                    int objectid = GetObjectId(name);
                    StartCoroutine(CaptureScreen(objectid));
                    DisplayText(name);

                }
            }
        }
    }
    public void DisplayText(string text)
    {
        camtext.color = new Color(255, 255, 255, 255);
        camtext.text = text;
        timer = true;
        time = 0f;
    }
    public int GetObjectId(string text)
    {
        int id = 0;
        for (int i = 0; i <= database.organisms.Count-1; i++)
        {
            if (text == database.organisms[i].name)
            {
                database.organisms[i].isCaptured = true;
                id = i;
            }
        }
        return id;
    }
    public string GetName(Sprite sprite)
    {
        string objectName = "";
        foreach (var item in database.organisms)
        {
            if (item.sprite == sprite)
            {
                objectName = item.name;
            }
        }
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
        database.organisms[objectid].sprite = sprite;
        sprites.Add(sprite);
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = true;
    }
}
