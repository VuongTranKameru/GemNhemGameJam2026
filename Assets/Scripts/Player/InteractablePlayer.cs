using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] InputCharacterManager input;
    [SerializeField] MainPlayerController playerSprite;

    [Header("Dialogue Controller")]
    bool isJustStartTalk;

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Cook") && obj.GetComponent<CookPlacement>().IsCountIngredient > 0)
        {
            playerSprite.EnableStartingCircleWhileCooking();
            if (input.PlayerInput.Cook.IsPressed())
                playerSprite.EnableLoadingCircleWhileCooking();
        }

        if (obj.CompareTag("NPC"))
            if (!isJustStartTalk)
                if (input.PlayerInput.Interact.IsPressed())
                {
                    input.IsTalkWithNpcRn = true;
                    isJustStartTalk = true;
                }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Cook"))
            playerSprite.DisableCircleWhileCooking();
    }

    public IEnumerator DelayABitAfterTheInteract()
    {
        yield return new WaitForSeconds(.2f);
        isJustStartTalk = false;
    }
}
