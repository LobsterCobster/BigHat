using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Dialogue : MonoBehaviour
{
    public GameObject ExclamationPoint;

    public GameObject photo;

    public GameObject Scrapbook;

    public GameObject gameManager;

    public DialogueTree dialoguetree;

    float charactersPerSecond = 10;

    private int questNumber = 0;

    private bool questsComplete = false;

    public GameObject player;

    private bool success;

    private bool fact;

    string[] script;

    public int index;

    private bool missionActive = false;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        QuestActive();
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
        gameManager.GetComponent<ChangeScene>().openCanvas("ScrapBook");
        string name = GameObject.Find("Frame").GetComponent<FrameReference>().organism.Name;
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
            missionActive = true;
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
        missionActive = false;
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
        gameManager.GetComponent<GameManager>().QuestComplete();
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
    public void QuestActive()
    {
        if (missionActive)
        {
            Scrapbook.transform.Find("Submit").gameObject.SetActive(true);
            ExclamationPoint.gameObject.SetActive(true);
        }
        else if (!missionActive & !questsComplete)
        {
            Scrapbook.transform.Find("Submit").gameObject.SetActive(false);
            ExclamationPoint.gameObject.SetActive(true);
        }
        else 
        {
            Scrapbook.transform.Find("Submit").gameObject.SetActive(false);
            ExclamationPoint.gameObject.SetActive(false);
        }
    }
}
