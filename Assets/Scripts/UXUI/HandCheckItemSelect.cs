using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCheckItemSelect : MonoBehaviour
{
    [SerializeField] ItemHolded handPlayer;
    [SerializeField] ItemSelectedBoard selectedBoard;

    private void OnEnable()
    {
        if (selectedBoard == null)
            selectedBoard = FindAnyObjectByType<ItemSelectedBoard>();

        if (handPlayer.IsItemSpriteAvailable)
            selectedBoard.EnabledShowItemHoldByPlayer(handPlayer.TakeIngredient, true);
    }

    private void OnDisable()
    {
        selectedBoard.EnabledShowItemHoldByPlayer(null, false);
    }
}
