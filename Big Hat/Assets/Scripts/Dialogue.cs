using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class Dialogue : MonoBehaviour
{

    public GameObject photo;

    public GameObject Scrapbook;

    public DialogueTree dialoguetree;

    float charactersPerSecond = 10;

    private int questNumber = 0;

    private bool questsComplete = false;

    public bool dialogueStart;

    public GameObject player;

    private int lineNumber = 0;

    string[] script;

    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueStart)
        {
            player.GetComponent<PlayerMove>().enabled = false;
            dialogueBox.SetActive(true);
            StartCoroutine(Quest(script));
            dialogueStart = false;

        }
    }
    public void Determine()
    {
        Scrapbook.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
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
    public void Talk()
    {
        script = dialoguetree.dialoguesection[questNumber].questdialogue;
        dialogueStart = true;
    }
    public void Success()
    {
        script = dialoguetree.dialoguesection[questNumber].success;
        dialogueStart = true;
    }
    public void Failure()
    {
        script = dialoguetree.dialoguesection[questNumber].failure;
        dialogueStart = true;
    }
    public void End()
    {
        script = dialoguetree.questEnd;
        dialogueStart = true;
    }
    IEnumerator Quest(string[] script)
    {
        foreach (var line in script)
        {
            string textBuffer = null;
            foreach (char c in line)
            {
                textBuffer += c;
                dialogueText.text = textBuffer;
                yield return new WaitForSeconds(1 / charactersPerSecond);
            }
            yield return new WaitForSeconds(1);
        }
        dialogueBox.SetActive(false);
        player.GetComponent<PlayerMove>().enabled = true;

    }
}
