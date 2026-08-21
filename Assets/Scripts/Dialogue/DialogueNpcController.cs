using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNpcController : MonoBehaviour
{
    [Header("Dialogue Customize")]
    [SerializeField] List<SODialogueConfig> scriptsOfNpc = new();

    [Header("UI")]
    [SerializeField] DialogueManager diagMana;
    bool isNotHaveDialYet;

    private void Awake()
    {
        if (diagMana == null)
            diagMana = FindAnyObjectByType<DialogueManager>();

        isNotHaveDialYet = true;
        IsDialogueAvailable();
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player") && player.GetComponentInParent<InputCharacterManager>().IsTalkWithNpcRn)
        {
            if (isNotHaveDialYet)
                StartTheDialogueManager();

            IsInputEndConverstation(player);
        }
    }

    void IsDialogueAvailable()
    {
        if ((scriptsOfNpc.Count > 0 && scriptsOfNpc[scriptsOfNpc.Count - 1].IsDialogueUseOnce) || scriptsOfNpc.Count == 0)
            GetComponent<CircleCollider2D>().enabled = false;
    }

    void StartTheDialogueManager()
    {
        foreach (SODialogueConfig kichBn in scriptsOfNpc)
            if (!kichBn.IsDialogueUseOnce)
            {
                diagMana.PutLinesOnFrame = kichBn.UseOnceDialogueLines;
                isNotHaveDialYet = diagMana.IsEndTheDialogueLines; //false
                break;
            }
    }

    protected void IsInputEndConverstation(Collider2D player)
    {
        if (diagMana.IsEndTheDialogueLines)
        {
            isNotHaveDialYet = diagMana.IsEndTheDialogueLines;
            player.GetComponentInParent<InputCharacterManager>().IsTalkWithNpcRn = false;
            StartCoroutine(player.GetComponent<InteractablePlayer>().DelayABitAfterTheInteract());

            IsDialogueAvailable();
        }
    }
}
