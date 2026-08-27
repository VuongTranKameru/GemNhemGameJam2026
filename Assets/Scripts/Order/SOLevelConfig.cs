using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "ScriptableObjects/Level")]
public class SOLevelConfig : ScriptableObject
{
    [SerializeField] internal float workTime;
    [SerializeField] internal List<SOFoodConfig> foodOrderList;
}
