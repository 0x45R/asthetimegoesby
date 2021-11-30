using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WriteText : MonoBehaviour
{
    public TMP_Text textMesh;

    public TMP_Text textCutsceneMesh;

    public string[]
        dialogue =
            new string[] {
                "Hi!",
                "Oh...",
                "I see",
                "you are now a small little caterpillar!",
                "Cute isn't it?",
                "Use WSAD to move...",
                "Oh... A gap?",
                "It is small though",
                "I think you could run through that hole.",
                "If you are stuck press R to restart"
            };

    public int[] wait = new int[] { 2, 2, 2, 4, 3, 5, 3, 3, 6, 3 };

    public bool waited = false;

    public bool gamestarted = false;

    public PlayerController player;

    public bool bosslevel;

    public Butterfly butterfly;

    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Coroutine dialogue = StartCoroutine(DisplayDialogue());
        Coroutine start = StartCoroutine(WaitForStart());
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && !waited)
        {
            StopAllCoroutines();
            audioSource.mute = true;
            waited = true;
            gamestarted = true;
            textMesh.text = dialogue[dialogue.Length - 1];
            textCutsceneMesh.text = "";
        }
        if (waited && bosslevel)
        {
            player.SendMessage("GotoSleep");
            butterfly.gameObject.SetActive(true);
            butterfly.SendMessage("WakeUp");
            bosslevel = false;
        }
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(5);
        gamestarted = true;
    }

    IEnumerator DisplayDialogue()
    {
        for (int i = 0; i < dialogue.Length; i++)
        {
            textMesh.text = dialogue[i];
            yield return new WaitForSeconds(wait[i]);
        }
        textCutsceneMesh.text = "";
        waited = true;
    }
}
