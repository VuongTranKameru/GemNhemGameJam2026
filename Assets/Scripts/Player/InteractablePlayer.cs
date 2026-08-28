using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePlayer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] InputCharacterManager input;
    [SerializeField] MainPlayerController playerSprite;

    [Header("Item Selected Board")]
    [SerializeField] ItemSelectedBoard selectedBoard;
    IngredientPlacement item;

    [Header("Dialogue Controller")]
    bool isJustStartTalk;

    private void Start()
    {
        if (selectedBoard == null)
            selectedBoard = FindAnyObjectByType<ItemSelectedBoard>();
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.TryGetComponent(out item))
            selectedBoard.EnabledShowIngredientInBox(item.IsThisIngredient, true);
    }

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
        if (obj.TryGetComponent(out item))
            selectedBoard.EnabledShowIngredientInBox(item.IsThisIngredient, false);

        if (obj.CompareTag("Cook"))
            playerSprite.DisableCircleWhileCooking();
    }

    public IEnumerator DelayABitAfterTheInteract()
    {
        yield return new WaitForSeconds(.2f);
        isJustStartTalk = false;
    }
}
