using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Dialogue : MonoBehaviour
{

    public GameObject photo;

    public GameObject Scrapbook;

    public GameObject GameManager;

    public DialogueTree dialoguetree;

    float charactersPerSecond = 10;

    private int questNumber = 0;

    private bool questsComplete = false;

    public GameObject player;

    private int lineNumber = 0;

    private bool success;

    private bool fact;

    string[] script;

    public int index;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (script != null)
            {
                if (dialogueText.text == script[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = script[index];
                }
            }
        }
        if (dialoguetree.dialoguesection.Length <= questNumber)
        {
            questNumber = 0;
            End();
        }
    }
    public void Determine()
    {
        GameManager.GetComponent<ChangeScene>().openCanvas("ScrapBook");
        string name = GameObject.Find("Frame").GetComponent<FrameReference>().organismReference;
        if (name == dialoguetree.dialoguesection[questNumber].answer)
        {
            Success();
        }
        else
        {
            Failure();
        }
    }
    public void StartDialogue()
    {
        StopAllCoroutines();
        player.GetComponent<PlayerMove>().enabled = false;
        dialogueBox.SetActive(true);
        index = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Quest(script));
    }
    public void Talk()
    {
        if (!questsComplete)
        {
            script = dialoguetree.dialoguesection[questNumber].questdialogue;
            Scrapbook.transform.Find("Submit").gameObject.SetActive(true);
            StartDialogue();
        }
        else
        {
            script = dialoguetree.questEnd;
            StartDialogue();
        }
    }
    public void Success()
    {
        script = dialoguetree.dialoguesection[questNumber].success;
        fact = true;
        StartDialogue();
    }
    public void Fact()
    {
        script = dialoguetree.dialoguesection[questNumber].fact;
        success = true;
        StartDialogue();
    }
    public void Failure()
    {
        script = dialoguetree.dialoguesection[questNumber].failure;
        StartDialogue();
    }
    public void End()
    {
        script = dialoguetree.questEnd;
        questsComplete = true;
        Scrapbook.transform.Find("Submit").gameObject.SetActive(false);
        questsComplete = true;
        GameObject.Find("Exclamation").gameObject.SetActive(false);
        StartDialogue();
    }
    IEnumerator Quest(string[] script)
    {
        foreach (char c in script[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(1 / charactersPerSecond);
        }

    }
    void NextLine()
    {
        if (index < script.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(Quest(script));
        }
        else
        {
            dialogueText.text = string.Empty;
            dialogueBox.SetActive(false);
            player.GetComponent<PlayerMove>().enabled = true;
            if (success)
            {
                questNumber++;
                success = false;
            }
            if (fact)
            {
                fact = false;
                Fact();
            }
        }
    }
}
