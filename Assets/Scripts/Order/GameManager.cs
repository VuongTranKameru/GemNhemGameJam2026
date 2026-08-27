using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SOLevelConfig levelSetting;

    [Header("Managers")]
    [SerializeField] InputCharacterManager inputMane;
    [SerializeField] EmployeeShiftManager shiftMane;
    [SerializeField] ShipPlacementManager orderMane;
    [SerializeField] CustomerOrderManager customerMane;

    void Awake()
    {
        if (inputMane == null || shiftMane == null || orderMane == null || customerMane == null)
        {
            inputMane = FindAnyObjectByType<InputCharacterManager>();
            shiftMane = FindAnyObjectByType<EmployeeShiftManager>();
            orderMane = FindAnyObjectByType<ShipPlacementManager>();
            customerMane = FindAnyObjectByType<CustomerOrderManager>();
        }

        SettingLevel();
    }

    void Update()
    {
        GameOverThePlayer();
    }

    #region Level Setup
    void SettingLevel()
    {
        shiftMane.SetWorkTime = levelSetting.workTime;
        orderMane.SetFoodOrders = levelSetting.foodOrderList;
    }
    #endregion

    #region Player Data Setting
    void GameOverThePlayer()
    {
        if (shiftMane.IsGameOverScene())
        {
            inputMane.GetComponentInChildren<MainPlayerController>().enabled = false;
            inputMane.PlayerInput.Disable();
            if (orderMane.IsOrderFinish <= 0)
                Debug.Log("win");
            else Debug.Log("lose");
        }
    }
    #endregion
}
