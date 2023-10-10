using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Photo;
using static UnityEngine.EventSystems.EventTrigger;

public class Photo : MonoBehaviour
{
    private Database database;
    private Camera cam;
    public Text camtext;
    public bool timer;

    private float time;
    Texture2D currentCapture;

    public Scrapbook scrapbook;

    public LayerMask capture;
    public SpriteList spriteList = new SpriteList();

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
        int level = 0;
        string name = "";
        RaycastHit hit;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, capture))
        {
            if (hit.transform.root.CompareTag("Capture"))
            {
                bool captured = false;
                name = hit.transform.root.name;
                foreach (var item in database.organisms)
                {
                    Debug.Log(item.name);
                    Debug.Log(name);
                    Debug.Log(item.isCaptured);
                    if (item.name == name && item.isCaptured == true)
                    {
                        captured = true;
                        break;
                    }
                    else if (item.name == name && item.isCaptured == false)
                    {
                        item.isCaptured = true;
                        break;
                    }
                }
                if (!captured)
                {
                    string scene = SceneManager.GetActiveScene().name;
                    if (scene == "Level1")
                    {
                        level = 1;
                    }
                    else if (scene == "Level2")
                    {
                        level = 2;
                    }
                    else if (scene == "Level3")
                    {
                        level = 3;
                    }
                    
                }
                else
                {
                    level = 0;
                }
            }
        }
        else
        {
            level = 0;
            name = spriteList.Miscellaneous.Count.ToString();
        }
        Debug.Log(level);
        StartCoroutine(CaptureScreen(level, name));
        DisplayText(name);
    }
    public void DisplayText(string text)
    {
        camtext.color = new Color(255, 255, 255, 255);
        camtext.text = text;
        timer = true;
        time = 0f;
    }
    public IEnumerator CaptureScreen(int level, string name)
    {
        yield return null;
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = false;

        yield return new WaitForEndOfFrame();

        currentCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        currentCapture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        currentCapture.Apply();
        Sprite sprite = Sprite.Create(currentCapture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));

        SpriteReference spriteReference = new SpriteReference();
        spriteReference.image = sprite;
        spriteReference.name = name;
        if (level == 1)
        {
            spriteList.Level1.Add(spriteReference);
        }
        else if (level == 2)
        {
            spriteList.Level2.Add(spriteReference);
        }
        else if (level == 3)
        {
            spriteList.Level3.Add(spriteReference);
        }
        else
        {
            spriteList.Miscellaneous.Add(spriteReference);
        }
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = true;
    }

    [System.Serializable]
    public class SpriteList
    {
        public List<SpriteReference> Level1;
        public List<SpriteReference> Level2;
        public List<SpriteReference> Level3;
        public List<SpriteReference> Miscellaneous;

    }
    [System.Serializable]
    public class SpriteReference
    {
        public Sprite image;
        public string name;
    }
}

