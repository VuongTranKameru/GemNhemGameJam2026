using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientSO", menuName = "ScriptableObjects/Ingredient")]
public class SOIngredientConfig : ScriptableObject
{
    [SerializeField] internal string nameIng;
    [SerializeField] internal Sprite picIng;
}