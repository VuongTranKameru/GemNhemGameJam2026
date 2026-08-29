using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionalDialogueManager : DialogueManager
{
    [SerializeField] SODialogueConfig dialogue;

    private void OnEnable()
    {
        PutLinesOnFrame = dialogue.NormalDialogueLines;
    }
}
