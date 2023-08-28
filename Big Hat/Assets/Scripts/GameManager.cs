using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public GameObject Photobutton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.enabled)
        {
            Photobutton.SetActive(true);
        }
        else
        {
            Photobutton.SetActive(false);
        }
    }
}
