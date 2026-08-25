using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodSO", menuName = "ScriptableObjects/Food")]
public class SOFoodConfig : SOIngredientConfig
{
    [SerializeField] internal List<SOIngredientConfig> listOfIngredient;
}
