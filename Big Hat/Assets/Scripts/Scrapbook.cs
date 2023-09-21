using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;
using System.Linq;

public class Scrapbook : MonoBehaviour
{
    public GameObject photo;
    public Text ObjectName;


    private Database database;
    private List<Sprite> sprites;

    public Sprite frame;
    // Start is called before the first frame update
    void Start()
    {
        database = GameObject.Find("DatabaseInstance").GetComponent<CreateDatabase>().database;
        sprites = photo.GetComponent<Photo>().sprites;
    }

    // Update is called once per frame
    void Update()
    {
        LoadImage();
    }
    public void LoadImage()
    {
        for (int i = 0; i < sprites.Count; i++)
        {
            int Id = i + 1;
            GameObject frame = GameObject.Find("Frame" + Id);
            if (frame.GetComponent<Image>().sprite == null)
            {
                Sprite sp =  sprites[i];
                frame.GetComponent<Image>().color = Color.white;
                frame.GetComponent<Image>().sprite = sp;
            }

        }
    }
    public void ClickImage()
    {
        GameObject image = GameObject.Find("Frame");
        GameObject child = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        frame = child.GetComponent<Image>().sprite;
        if (frame != null)
        {
            image.GetComponent<Image>().color = Color.white;
            image.GetComponent<Image>().sprite = frame;
            int id = int.Parse(string.Concat(child.name.Where(Char.IsDigit)));
            string name = photo.GetComponent<Photo>().GetName(frame);
            ObjectName.text = name;
        }
        else
        {
            image.GetComponent<Image>().color = Color.clear;
            image.GetComponent<Image>().sprite = null;
            ObjectName.text = "";
        }
    }
}
