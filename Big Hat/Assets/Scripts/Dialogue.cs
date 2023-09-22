using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (dialoguetree.dialoguesection.Length <= questNumber)
        {
            questNumber = 0;
            End();
        }
    }
    public void Determine()
    {
        GameManager.GetComponent<ChangeScene>().openCanvas("ScrapBook");
        Sprite frame = Scrapbook.GetComponent<Scrapbook>().frame;
        string name = photo.GetComponent<Photo>().GetName(frame);
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
        player.GetComponent<PlayerMove>().enabled = false;
        dialogueBox.SetActive(true);
        index = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(Quest(script));
    }
    public void Talk()
    {
        script = dialoguetree.dialoguesection[questNumber].questdialogue;
        StartDialogue();
    }
    public void Success()
    {
        script = dialoguetree.dialoguesection[questNumber].success;
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
        }
    }
}
