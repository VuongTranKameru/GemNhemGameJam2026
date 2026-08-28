using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CookPlacement : PlacementOfItem
{
    [SerializeField] SOIngredientConfig trash;

    [Header("Food Recipe")]
    [SerializeField] SOFoodConfig[] cookfoodList;
    List<SOIngredientConfig> ingredientAddIn;
    List<SOFoodConfig> exclusionRecipe;
    int countIng, countRecipe;

    public int IsCountIngredient { get => ingredientAddIn.Count; }

    private void Awake()
    {
        ingredientAddIn = new();
        exclusionRecipe = new();
    }

    private void OnTriggerStay2D(Collider2D obj)
    {
        if (obj.CompareTag("Player"))
        {
            if (obj.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
                if (playerHolder.IsItemSpriteAvailable)
                    AddNewIngIntoCook(playerHolder);

            if (obj.GetComponentInParent<InputCharacterManager>().PlayerInput.Cook.IsPressed())
                if (!playerHolder.IsItemSpriteAvailable && IsCountIngredient > 0)
                    obj.GetComponentInParent<InputCharacterManager>().PlayerInput.Cook.performed += CookIntoNewFood;
        }
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && playerHolder.IsItemSpriteAvailable)
            obj.GetComponentInParent<InputCharacterManager>().PlayerInput.Cook.performed -= CookIntoNewFood;
    }

    void AddNewIngIntoCook(ItemHolded item)
    {
        ingredientAddIn.Add(item.TakeIngredient);
        RemoveFoodOutOfHand();
    }

    void CookIntoNewFood(InputAction.CallbackContext ctx)
    {
        CheckFirstIngAndLengthOfRecipe();

        if (exclusionRecipe.Count > 0)
        {
            for (countIng = 1; countIng < IsCountIngredient; countIng++)
                CheckRecipe(ingredientAddIn[countIng]);

            InsertFoodIntoPlayerHand(exclusionRecipe[0]);
        }
        else InsertFoodIntoPlayerHand(trash);

        ingredientAddIn.Clear();
        exclusionRecipe.Clear();
    }

    void CheckFirstIngAndLengthOfRecipe()
    {
        foreach (SOFoodConfig cookbook in cookfoodList)
            if (cookbook.listOfIngredient.Count == IsCountIngredient)
                if (cookbook.listOfIngredient[0] == ingredientAddIn[0])
                    exclusionRecipe.Add(cookbook);
    }

    void CheckRecipe(SOIngredientConfig ing)
    {
        for (countRecipe = exclusionRecipe.Count - 1; countRecipe >= 0; countRecipe--)
            if (exclusionRecipe[countRecipe].listOfIngredient[countIng] != ing)
                exclusionRecipe.RemoveAt(countRecipe);
    }
}
