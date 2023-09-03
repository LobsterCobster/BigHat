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
    public Text Name;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LoadImage();
    }
    public void LoadImage()
    {
        for (int i = 0; i < photo.GetComponent<Photo>().images.Count; i++)
        {
            int id = i + 1;
            GameObject frame = GameObject.Find("Frame" + id);
            if (frame.GetComponent<Image>().sprite == null)
            {
                List<string> p = photo.GetComponent<Photo>().images;
                List<Sprite> sprites = photo.GetComponent<Photo>().sprites;
                Sprite[] spriteArray = sprites.ToArray();
                if (spriteArray.Length == id)
                {
                    Sprite sp = spriteArray[i];
                    frame.GetComponent<Image>().color = Color.white;
                    frame.GetComponent<Image>().sprite = sp;
                }
            }
        }
    }
    public void ClickImage()
    {
        GameObject image = GameObject.Find("Frame");
        GameObject child = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        Sprite frame = child.GetComponent<Image>().sprite;
        if (frame != null)
        {
            image.GetComponent<Image>().color = Color.white;
            image.GetComponent<Image>().sprite = frame;
            int id = int.Parse(string.Concat(child.name.Where(Char.IsDigit)));
            string name = photo.GetComponent<Photo>().GetName(id-1);
            Name.text = name;
        }
        else
        {
            image.GetComponent<Image>().color = Color.clear;
            image.GetComponent<Image>().sprite = null;
            Name.text = "";
        }
    }
}
