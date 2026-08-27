using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolded : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] SOIngredientConfig ingredient;

    [Header("Visual")]
    [SerializeField] SpriteRenderer spriteI;

    public SOIngredientConfig TakeIngredient { get => ingredient; }

    public SOIngredientConfig SetFoodPlacement { set => ingredient = value; }
    
    public Sprite ItemSprite { 
        set {
            if (spriteI.gameObject.activeInHierarchy)
                spriteI.gameObject.SetActive(false);
            else spriteI.gameObject.SetActive(true);
            spriteI.sprite = value; 
        }
    }

    public bool IsItemSpriteAvailable { get => spriteI.gameObject.activeInHierarchy; }

    public SOIngredientConfig IsFoodAvailable()
    {
        if (IsItemSpriteAvailable)
            if (ingredient.name.Contains("BANH"))
                return ingredient;

        return null;
    }
}
