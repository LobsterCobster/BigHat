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

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

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
        dialogueBox.SetActive(true);
        StartCoroutine(Example());
    }
    IEnumerator Example()
    {
        foreach (var script in script)
        {
            string textBuffer = null;
            foreach (char c in script)
            {
                textBuffer += c;
                dialogueText.text = textBuffer;
                yield return new WaitForSeconds(1 / charactersPerSecond);
            }
            yield return new WaitForSeconds(1);
        }
        dialogueEnd = true;
        if (dialogueEnd)
        {
            dialogueBox.SetActive(false);
            player.GetComponent<PlayerMove>().canMove = true;
        }

    }
}
