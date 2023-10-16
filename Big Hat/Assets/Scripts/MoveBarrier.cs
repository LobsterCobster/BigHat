using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBarrier : MonoBehaviour
{
    public GameObject Manager;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = Manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move()
    {
        if (gameManager.questComplete)
        {
            gameObject.SetActive(false);
        }
    }
}
