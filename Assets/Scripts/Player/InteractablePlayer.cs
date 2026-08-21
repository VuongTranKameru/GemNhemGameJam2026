using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] InputCharacterManager input;

    [Header("Dialogue Controller")]
    bool isJustStartTalk;

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("NPC"))
            if (!isJustStartTalk)
                if (input.PlayerInput.Interact.IsPressed())
                {
                    input.IsTalkWithNpcRn = true;
                    isJustStartTalk = true;
                }
    }

    public IEnumerator DelayABitAfterTheInteract()
    {
        yield return new WaitForSeconds(.2f);
        isJustStartTalk = false;
    }
}
