using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashPlacement : PlacementOfItem
{
    private void OnTriggerStay2D(Collider2D food)
    {
        if (food.CompareTag("Player"))
        {
            if (food.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
                if (playerHolder.IsItemSpriteAvailable)
                    RemoveFoodOutOfHand();
        }
    }
}
