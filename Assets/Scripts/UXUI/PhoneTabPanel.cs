using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTabPanel : MonoBehaviour
{
    [SerializeField] CustomerOrderManager customerMane;
    int oldOrdersTotal;
    bool isHaveNewOrder;

    private void Awake()
    {
        if (customerMane == null)
            customerMane = GetComponentInParent<CustomerOrderManager>();
    }

    private void OnEnable()
    {
        if (oldOrdersTotal != customerMane.ImageOrderList.Count)
        {
            isHaveNewOrder = true;
            oldOrdersTotal = customerMane.ImageOrderList.Count;
        }
    }

    private void Update()
    {
        if (isHaveNewOrder)
        {
            foreach (NewMessageImg mes in customerMane.ImageOrderList)
            {
                if (!mes.isLoadYet)
                    customerMane.AddNewOrderIntoUiBar(mes);

                if (mes.isChanged)
                    customerMane.ChangeAlphaFoodImage(mes);
            }

            isHaveNewOrder = false;
        }
    }
}
