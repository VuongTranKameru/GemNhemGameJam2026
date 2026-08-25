using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPlacement : PlacementOfItem
{
    [SerializeField] SOIngredientConfig ingredient;

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player") && player.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
            InsertFoodIntoPlayerHand(ingredient);
    }
}
