using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerOrderManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] Transform orderContent;
    [SerializeField] GameObject orderUiPrefab;
    [SerializeField] List<Image> imageOrderList = new();
    GameObject tempOrder;
    int countImage;

    public void AddNewOrderIntoUiBar(SOFoodConfig food)
    {
        tempOrder = Instantiate(orderUiPrefab, orderContent);
        tempOrder.GetComponent<Image>().sprite = food.picIng;
        imageOrderList.Add(tempOrder.GetComponent<Image>());
    }

    public void RemoveFinishedOrderInBar(SOFoodConfig food)
    {
        for (countImage = imageOrderList.Count - 1; countImage >= 0; countImage--)
            if (imageOrderList[countImage].sprite == food.picIng)
            {
                Destroy(imageOrderList[countImage].gameObject);
                imageOrderList.RemoveAt(countImage);
            }
    }
}
