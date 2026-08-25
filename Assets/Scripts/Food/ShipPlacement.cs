using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacement : PlacementOfItem
{
    [SerializeField] ItemHolded[] orderList;

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player") && player.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
            if (playerHolder.IsFoodAvailable() != null)
                foreach (ItemHolded order in orderList)
                    if (order.TakeIngredient == null)
                    {
                        InsertOrderOnTable(orderList[0], playerHolder.IsFoodAvailable());
                        RemoveFoodOutOfHand();
                        break;
                    }
    }

    void InsertOrderOnTable(ItemHolded holder, SOIngredientConfig food)
    {
        holder.TakeIngredient = food;
        holder.ItemSprite = food.picIng;
    }
}
