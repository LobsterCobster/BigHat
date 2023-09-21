using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueAsset dialogueAsset;
    public string[] script;

    float charactersPerSecond = 5;

    bool dialogueEnd;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        script = dialogueAsset.dialogue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Talk()
    {
        dialogueEnd = false;
        StartCoroutine(Example());
        
    }
    IEnumerator Example()
    {
        foreach (var script in script)
        {
            Debug.Log(script.ToString());
            yield return new WaitForSeconds(charactersPerSecond);
        }
        dialogueEnd = true;
        if (dialogueEnd)
        {
            Debug.Log("End");
            player.GetComponent<PlayerMove>().canMove = true;
        }

    }
}
