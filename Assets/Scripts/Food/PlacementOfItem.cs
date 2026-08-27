using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementOfItem : MonoBehaviour
{
    [SerializeField] protected ItemHolded playerHolder;

    private void Start()
    {
        if (playerHolder == null)
            playerHolder = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<ItemHolded>();
    }

    protected void InsertFoodIntoPlayerHand(SOIngredientConfig config)
    {
        if (!playerHolder.IsItemSpriteAvailable)
        {
            playerHolder.SetFoodPlacement = config;
            playerHolder.ItemSprite = config.picIng;
        }
    }

    protected void RemoveFoodOutOfHand()
    {
        if (playerHolder.IsItemSpriteAvailable)
        {
            playerHolder.SetFoodPlacement = null;
            playerHolder.ItemSprite = null;
        }
    }
}
