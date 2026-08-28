using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckingOrder
{
    internal OrderTime foodOrder;
    internal bool isPlaceAnOrder, isDone;

    public CheckingOrder (OrderTime setting)
    {
        foodOrder = setting;
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] SOLevelConfig levelSetting;
    List<CheckingOrder> checkOrderList;
    int countTime;

    [Header("Managers")]
    [SerializeField] InputCharacterManager inputMane;
    [SerializeField] EmployeeShiftManager shiftMane;
    [SerializeField] ShipPlacementManager orderMane;
    [SerializeField] CustomerOrderManager customerMane;
    [SerializeField] GameObject specialRecipe;

    [Header("Scenes")]
    [SerializeField] UnityEvent winEvent;
    [SerializeField] UnityEvent loseEvent;

    void Awake()
    {
        if (inputMane == null || shiftMane == null || orderMane == null || customerMane == null)
        {
            inputMane = FindAnyObjectByType<InputCharacterManager>();
            shiftMane = FindAnyObjectByType<EmployeeShiftManager>();
            orderMane = FindAnyObjectByType<ShipPlacementManager>();
            customerMane = FindAnyObjectByType<CustomerOrderManager>();
        }

        if (specialRecipe == null)
            specialRecipe = GameObject.FindGameObjectWithTag("Finish");

        SettingLevel();
    }

    void Update()
    {
        if (shiftMane.GetWorkTime > 0 && shiftMane.IsPlayerInShift)
            CheckTimeToPutNewOrder();

        if (shiftMane.IsGameOverScene())
            GameOverThePlayer();
    }

    #region Level Setup
    void SettingLevel()
    {
        shiftMane.SetWorkTime = levelSetting.workTime;

        if (levelSetting.ActiveSpecialRecipe)
            specialRecipe.SetActive(true);
        else specialRecipe.SetActive(false);

        if (checkOrderList == null)
            checkOrderList = new();
        foreach (OrderTime orderSetting in levelSetting.foodOrderList)
            checkOrderList.Add(new CheckingOrder(orderSetting));

        customerMane.CheckAmountOfMessagesInADay(checkOrderList.Count);

        countTime = 0;
    }

    void CheckTimeToPutNewOrder()
    {
        if (checkOrderList.Count > 0)
        {
            if (shiftMane.GetWorkTime == checkOrderList[countTime].foodOrder.time)
                if (!checkOrderList[countTime].isPlaceAnOrder && !checkOrderList[countTime].isDone)
                {
                    orderMane.SetFoodOrder = checkOrderList[countTime].foodOrder.food;
                    //customerMane.TakeNewOrderInPhone(levelSetting.foodOrderList[countTime].food);
                    customerMane.TakeNewOrderAndAddIntoUiBar(levelSetting.foodOrderList[countTime].food);
                    checkOrderList[countTime].isPlaceAnOrder = true;
                    orderMane.IsCheckNewOrderYet = true;
                    if (countTime < checkOrderList.Count - 1)
                        countTime++;
                }
        }
    }

    public void FinishOrder(SOFoodConfig doneOrder)
    {
        foreach (CheckingOrder orders in checkOrderList)
            if (orders.foodOrder.food == doneOrder)
            {
                customerMane.BlurryFinishedOrderInBar(orders.foodOrder.food, checkOrderList.Count);
                orders.isDone = true;
                break;
            }
    }
    #endregion

    #region Player Data Setting
    void GameOverThePlayer()
    {
        inputMane.GetComponentInChildren<MainPlayerController>().enabled = false;
        inputMane.PlayerInput.Disable();
        if (orderMane.IsOrderFinish <= 0)
            StartCoroutine(DeteminatePlayerDestiny(winEvent));
        else StartCoroutine(DeteminatePlayerDestiny(loseEvent));
    }

    IEnumerator DeteminatePlayerDestiny(UnityEvent e)
    {
        yield return new WaitForSeconds(3f);
        e.Invoke();
    }
    #endregion
}
