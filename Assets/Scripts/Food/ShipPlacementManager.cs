using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacementManager : PlacementOfItem
{
    [Header("Shipping Order Manager")]
    List<SOFoodConfig> foodOrdersInDay = new();
    bool isNewFoodWrapped;

    [Header("Food")]
    [SerializeField] ItemHolded[] wrappedFoodSlots;
    int countSlot, countOrder;

    public List<SOFoodConfig> SetFoodOrders
    {
        set
        {
            if (foodOrdersInDay == null)
                foodOrdersInDay = new();
            foodOrdersInDay = value;
        }
    }

    public int IsOrderFinish { get => foodOrdersInDay.Count; }

    private void Awake()
    {
        if (foodOrdersInDay == null)
            foodOrdersInDay = new();

        if (wrappedFoodSlots == null)
            Debug.LogWarning("--Remember to put the slot in!--");
    }

    private void Update()
    {
        TakingOrdersFromShippers();
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player") && player.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
            if (playerHolder.IsFoodAvailable() != null)
                foreach (ItemHolded order in wrappedFoodSlots)
                    if (order.TakeIngredient == null)
                    {
                        InsertOrderOnTable(wrappedFoodSlots[0], playerHolder.IsFoodAvailable());
                        RemoveFoodOutOfHand();
                        break;
                    }
    }

    void InsertOrderOnTable(ItemHolded holder, SOIngredientConfig food)
    {
        holder.SetFoodPlacement = food;
        holder.ItemSprite = food.picIng;
        isNewFoodWrapped = true;
    }

    void TakingOrdersFromShippers()
    {
        if (isNewFoodWrapped)
        {
            countSlot = wrappedFoodSlots.Length - 1;
            foreach (ItemHolded slot in wrappedFoodSlots)
            {
                for (countOrder = foodOrdersInDay.Count - 1; countOrder >= 0; countOrder --)
                    if (CheckOrderIfCorrect( (SOFoodConfig)wrappedFoodSlots[countSlot].TakeIngredient, foodOrdersInDay[countOrder] ))
                    {
                        Debug.Log("done");
                        foodOrdersInDay.RemoveAt(countOrder);
                        slot.ItemSprite = null;
                        slot.SetFoodPlacement = null;
                        wrappedFoodSlots[countSlot] = null;
                        break;
                    }
                countSlot--;
            }
            isNewFoodWrapped = false;
        }
    }

    bool CheckOrderIfCorrect(SOFoodConfig food, SOFoodConfig order)
    {
        if (food == order)
            return true;
        return false;
    }
}
