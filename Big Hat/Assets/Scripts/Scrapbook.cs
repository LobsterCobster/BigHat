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
    public Text ScientificName;
    public Text PageTitle;


    private Database database;
    [SerializeField]
    private PhotoSO photoSO;

    public Sprite frame;

    private int pageNumber;

    public List<PhotoSO.SpriteReference> sprites = new List<PhotoSO.SpriteReference>();


    // Start is called before the first frame update
    void Start()
    {
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
            sprites = photoSO.spriteList.Level1.ToList();
        }
        else if (pageNumber == 2)
        {
            sprites = photoSO.spriteList.Level2.ToList();
        }
        else if (pageNumber == 3)
        {
            sprites = photoSO.spriteList.Level3.ToList();
        }
        else
        {
            sprites = photoSO.spriteList.Miscellaneous.ToList();
        }
        LoadImage();
        LoadPageTitle();
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
                    frame.GetComponent<FrameReference>().GetOrganismReference(sprites[i].name);
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
                        Sprite sp = sprites[i + (6 * spriteNumber)].image;
                        frame.GetComponent<Image>().color = Color.white;
                        frame.GetComponent<Image>().sprite = sp;
                        frame.GetComponent<FrameReference>().GetOrganismReference(sprites[i].name);
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
            frame.GetComponent<FrameReference>().organism = null;
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
            if (child.GetComponent<FrameReference>().mis == false)
            {
                image.GetComponent<FrameReference>().GetOrganismReference(child.GetComponent<FrameReference>().organism.Name);
                int id = int.Parse(string.Concat(child.name.Where(Char.IsDigit)));
                string name = image.GetComponent<FrameReference>().organism.Name;
                string scientificName = image.GetComponent<FrameReference>().organism.ScientificName;
                ObjectName.text = name;
                ScientificName.text = scientificName;
            }
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
    public void LoadPageTitle()
    {
        if (pageNumber == 1)
        {
            PageTitle.text = "Level 1";
        }
        else if (pageNumber == 2)
        {
            PageTitle.text = "Level 2";
        }
        else if (pageNumber == 3)
        {
            PageTitle.text = "Level 3";
        }
        else
        {
            PageTitle.text = $"Miscellaneous {pageNumber - 3}";
        }
    }
    public void RevertScrapbook()
    {
        pageNumber = 1;
        GameObject image = GameObject.Find("Frame");
        image.GetComponent<Image>().color = Color.clear;
        image.GetComponent<Image>().sprite = null;
        ClearImage();
        ObjectName.text = "";
        ScientificName.text = "";
        transform.Find("Submit").GetComponent<Button>().interactable = false;
    }
    public void SetSubmissionButtonOn()
    {
        transform.Find("Submit").gameObject.SetActive(true);
    }
    public void SetSubmissionButtonOf()
    {
        if (transform.Find("Submit").gameObject.activeSelf == true)
        {
            transform.Find("Submit").gameObject.SetActive(false);
        }
    }
}
