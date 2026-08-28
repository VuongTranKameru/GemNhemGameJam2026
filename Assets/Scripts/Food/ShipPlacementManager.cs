using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPlacementManager : PlacementOfItem
{
    [Header("Shipping Order Manager")]
    [SerializeField] GameManager gameMane;
    List<SOFoodConfig> foodOrdersInDay = new();
    bool isNewFoodWrapped; //key to check order, remember

    [Header("Food")]
    [SerializeField] ItemHolded[] wrappedFoodSlots;
    int countOrder;

    public SOFoodConfig SetFoodOrder
    {
        set
        {
            if (foodOrdersInDay == null)
                foodOrdersInDay = new();
            foodOrdersInDay.Add(value);
        }
    }

    public bool IsCheckNewOrderYet { set => isNewFoodWrapped = value; }
    public int IsOrderFinish { get => foodOrdersInDay.Count; }

    private void Awake()
    {
        if (foodOrdersInDay == null)
            foodOrdersInDay = new();

        if (gameMane == null)
            gameMane = FindAnyObjectByType<GameManager>();

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
                        InsertOrderOnTable(order, playerHolder.IsFoodAvailable());
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
            foreach (ItemHolded slot in wrappedFoodSlots)
            {
                for (countOrder = foodOrdersInDay.Count - 1; countOrder >= 0; countOrder --)
                    if (CheckOrderIfCorrect( (SOFoodConfig)slot.TakeIngredient, foodOrdersInDay[countOrder] ))
                    {
                        gameMane.FinishOrder(foodOrdersInDay[countOrder]);
                        foodOrdersInDay.RemoveAt(countOrder);
                        slot.ItemSprite = null;
                        slot.SetFoodPlacement = null;
                        break;
                    }
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
