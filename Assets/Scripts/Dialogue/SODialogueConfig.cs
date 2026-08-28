using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfDialogue
{
    OnlyTalk,
    ChoiceMatter
}

[System.Serializable]
public class CharacterLines
{
    [SerializeField] internal string character;
    [SerializeField] internal string line;
    [SerializeField] internal Sprite ava;
}

[CreateAssetMenu(fileName = "DialogueScriptData", menuName = "ScriptableObjects/DialogueLines")]
public class SODialogueConfig : ScriptableObject
{
    [Header("Dialogue Lines")]
    [SerializeField] TypeOfDialogue type;
    [SerializeField] internal string nameDialogue;
    [SerializeField] bool isUsedOnce, lockThisDialogue;
    [SerializeField] CharacterLines[] dialogueLines;

    public TypeOfDialogue TypeOfDialog { get { return type; } }
    public bool IsDialogueUseOnce { get => lockThisDialogue; }

    public CharacterLines[] NormalDialogueLines { get => dialogueLines; }
    public CharacterLines[] UseOnceDialogueLines
    {
        get
        {
            if (isUsedOnce)
                lockThisDialogue = true;

            return dialogueLines;
        }
    }
}
