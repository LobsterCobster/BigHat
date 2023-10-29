using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public GameObject player;
    public GameObject gameManager;
    public string LevelName;
    // Start is called before the first frame update

    private void Update()
    {
        if (gameManager.GetComponent<GameManager>().questComplete)
        {
            gameManager.GetComponent<GameManager>().EndLevel(LevelName);
        }
    }
}
