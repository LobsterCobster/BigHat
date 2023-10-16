using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public Canvas canvas;
    public Canvas cameraCanvas;
    public bool questComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (cam.enabled)
        {
            canvas.enabled = false;
            cameraCanvas.enabled = true;
        }
        else
        {
            canvas.enabled = true;
            cameraCanvas.enabled = false;
        }
    }
    public void GetNPCDialogue()
    {
        GameObject NPC = GameObject.FindGameObjectWithTag("NPC");
        NPC.GetComponent<Dialogue>().Determine();
    }
    public void QuestComplete()
    {
        questComplete = true;
    }
    public void EndLevel(string LevelName)
    {
        StartCoroutine(LevelWipe(LevelName));
    }
    IEnumerator LevelWipe(string LevelName)
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<ChangeScene>().loadScene(LevelName);

    }
}
