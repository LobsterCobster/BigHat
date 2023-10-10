using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;
using System.Linq;
using static Photo;
using UnityEngine.SceneManagement;

public class Scrapbook : MonoBehaviour
{
    public GameObject photo;
    public Text ObjectName;


    private Database database;
    private SpriteList spriteList = new SpriteList();

    public Sprite frame;

    private int pageNumber;

    public List<SpriteReference> sprites = new List<SpriteReference>();

    // Start is called before the first frame update
    void Start()
    {
        spriteList = photo.GetComponent<Photo>().spriteList;
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Level1")
        {
            pageNumber = 1;
        }
        else if (scene == "Level2")
        {
            pageNumber = 2;
        }
        else if (scene == "Level3")
        {
            pageNumber = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pageNumber == 1)
        {
            sprites = spriteList.Level1.ToList();
        }
        else if (pageNumber == 2)
        {
            sprites = spriteList.Level2.ToList();
        }
        else if (pageNumber == 3)
        {
            sprites = spriteList.Level3.ToList();
        }
        else
        {
            sprites = spriteList.Miscellaneous.ToList();
        }
        LoadImage();
    }
    public void LoadImage()
    {
        if (pageNumber <= 3)
        {
            for (int i = 0; i < sprites.Count; i++)
            {
                int Id = i + 1;
                GameObject frame = GameObject.Find("Frame" + Id);
                if (frame.GetComponent<Image>().sprite == null)
                {
                    Sprite sp = sprites[i].image;
                    frame.GetComponent<Image>().color = Color.white;
                    frame.GetComponent<Image>().sprite = sp;
                    frame.GetComponent<FrameReference>().GetFrameName(sprites[i].name);
                }

            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                int spriteNumber = pageNumber - 4;
                int Id = i + 1;
                GameObject frame = GameObject.Find("Frame" + Id);
                if (frame.GetComponent<Image>().sprite == null)
                {
                    if (i + (6 * spriteNumber) < sprites.Count)
                    {
                        Debug.Log(i + (6 * spriteNumber));
                        Debug.Log(sprites.Count);
                        Sprite sp = sprites[i + (6 * spriteNumber)].image;
                        frame.GetComponent<Image>().color = Color.white;
                        frame.GetComponent<Image>().sprite = sp;
                        frame.GetComponent<FrameReference>().GetFrameName(sprites[i].name);
                    }
                }
            }
        }
    }
    public void ClearImage()
    {
        for (int i = 1; i <= 6 ; i++)
        {
            GameObject frame = GameObject.Find("Frame" + i);
            frame.GetComponent<Image>().color = Color.clear;
            frame.GetComponent<Image>().sprite = null;
            frame.GetComponent<FrameReference>().organismReference = null;
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
            image.GetComponent<FrameReference>().GetFrameName(child.GetComponent<FrameReference>().organismReference);
            int id = int.Parse(string.Concat(child.name.Where(Char.IsDigit)));
            string name = image.GetComponent<FrameReference>().organismReference;
            ObjectName.text = name;
            transform.Find("Submit").GetComponent<Button>().interactable = true;
        }
        else
        {
            image.GetComponent<Image>().color = Color.clear;
            image.GetComponent<Image>().sprite = null;
            ObjectName.text = "";
            transform.Find("Submit").GetComponent<Button>().interactable = false;
        }
    }
    public void PageRight()
    {
        pageNumber++;
        ClearImage();
    }
    public void PageLeft()
    {
        if (pageNumber > 1)
        {
            pageNumber--;
        }
        ClearImage();
    }
}
