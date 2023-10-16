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

    [SerializeField]
    private PhotoSO photoSO;

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
        Organism.Level level = Organism.Level.None;
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
                    if (item.name == name && item.isCaptured == true)
                    {
                        captured = true;
                        break;
                    }
                    else if (item.name == name && item.isCaptured == false)
                    {
                        item.isCaptured = true;
                        level = item.level;
                        break;
                    }
                }
            }
        }
        else
        {
            name = photoSO.spriteList.Miscellaneous.Count.ToString();
        }
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
    public IEnumerator CaptureScreen(Organism.Level level, string name)
    {
        yield return null;
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = false;

        yield return new WaitForEndOfFrame();

        currentCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        currentCapture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
        currentCapture.Apply();
        Sprite sprite = Sprite.Create(currentCapture, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));

        PhotoSO.SpriteReference spriteReference = new PhotoSO.SpriteReference();
        spriteReference.image = sprite;
        spriteReference.name = name;
        if (level == Organism.Level.Level1)
        {
            photoSO.spriteList.Level1.Add(spriteReference);
        }
        else if (level == Organism.Level.Level2)
        {
            photoSO.spriteList.Level2.Add(spriteReference);
        }
        else if (level == Organism.Level.Level3)
        {
            photoSO.spriteList.Level3.Add(spriteReference);
        }
        else
        {
            photoSO.spriteList.Miscellaneous.Add(spriteReference);
        }
        GameObject.Find("CameraBackground").GetComponent<Canvas>().enabled = true;
    }
}

