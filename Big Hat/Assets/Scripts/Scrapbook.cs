using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor.Compilation;

public class Scrapbook : MonoBehaviour
{
    public GameObject photo;
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
            List<string> p = photo.GetComponent<Photo>().images;
            int id = i + 1;
            string dataPath = Application.persistentDataPath;
            string fileName = id + ".png";
            
            fileName = dataPath + "/" + fileName;

            Debug.Log(fileName);

            if (File.Exists(fileName))
            {
                Debug.Log("File exists");
            }
            else
            {
                Debug.Log("File does not exist");
            }

        }
    }
}
