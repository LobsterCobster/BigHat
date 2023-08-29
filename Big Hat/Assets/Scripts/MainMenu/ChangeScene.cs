using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void loadScene(string levelname)
    {
        SceneManager.LoadScene(levelname);
    }
    public void openCanvas(string canvasName) 
    { 
        canvas = GameObject.Find(canvasName).GetComponent<Canvas>();
        if (canvas.enabled == false)
        {
            canvas.enabled = true;
            Time.timeScale = 0;


        }
        else
        {
            canvas.enabled = false;
            Time.timeScale = 1;
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
}
