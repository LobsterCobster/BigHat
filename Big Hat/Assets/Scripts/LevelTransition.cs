using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public string LevelName;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if (gameManager.GetComponent<GameManager>().questComplete)
        {
            gameManager.GetComponent<GameManager>().EndLevel(LevelName);
        }
    }
}
