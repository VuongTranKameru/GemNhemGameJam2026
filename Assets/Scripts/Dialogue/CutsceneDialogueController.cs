using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneDialogueController : MonoBehaviour
{
    [SerializeField] SODialogueConfig dialogueScripts;

    [Header("Manager")]
    [SerializeField] DialogueManager dialgMane;
    [SerializeField] UnityEvent specialEvent;
    [SerializeField] float timer;

    private void Awake()
    {
        if (dialgMane != null)
        {
            if (timer <= 0)
                dialgMane.PutLinesOnFrame = dialogueScripts.NormalDialogueLines;
            else StartCoroutine(TimerDialogue());
        }
    }

    IEnumerator TimerDialogue()
    {
        yield return new WaitForSeconds(timer);
        specialEvent.Invoke();
        dialgMane.PutLinesOnFrame = dialogueScripts.NormalDialogueLines;
    }
}
