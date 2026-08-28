using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OrderTime
{
    [SerializeField] internal SOFoodConfig food;
    [SerializeField] internal int time;
}

[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/Level")]
public class SOLevelConfig : ScriptableObject
{
    [SerializeField] internal float workTime;
    [SerializeField] internal List<OrderTime> foodOrderList;
    [SerializeField] bool isSpecialRecipe;

    public bool ActiveSpecialRecipe {  get => isSpecialRecipe; }
}
