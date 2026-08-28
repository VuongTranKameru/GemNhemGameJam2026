using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewMessageImg
{
    internal SOFoodConfig food;
    internal GameObject rawObj;
    internal bool isLoadYet, isChanged;

    public NewMessageImg(SOFoodConfig f)
    {
        food = f;
        isLoadYet = false;
        isChanged = false;
    }

    public NewMessageImg(SOFoodConfig f, GameObject obj)
    {
        food = f;
        rawObj = obj;
        isLoadYet = false;
    }
}

public class CustomerOrderManager : MonoBehaviour
{
    [Header("UI Bar")]
    [SerializeField] Transform orderContent;
    [SerializeField] GameObject orderUiPrefab;
    List<NewMessageImg> imageOrderList = new();
    GameObject tempOrder;
    Color tmpColor;

    [Header("Phone Tab")]
    [SerializeField] TMP_Text amountMesTxt;
    [SerializeField] Transform mesContent;
    [SerializeField] GameObject bubblePrefab;
    GameObject samplBubble;
    int finishMes;

    [Header("Sfx")]
    [SerializeField] AudioSource sfxDoneOrder;

    public List<NewMessageImg> ImageOrderList { 
        get => imageOrderList;  
    }

    #region UI BAR
    public void TakeNewOrderInPhone(SOFoodConfig food)
    {
        imageOrderList.Add(new NewMessageImg(food));
        AddNewMessageOrder(food);
    }

    public void AddNewOrderIntoUiBar(NewMessageImg mes)
    {
        tempOrder = Instantiate(orderUiPrefab, orderContent);
        tempOrder.GetComponentInChildren<Image>().sprite = mes.food.picIng;
        mes.rawObj = tempOrder;
        mes.isLoadYet = true;
    }

    public void TakeNewOrderAndAddIntoUiBar(SOFoodConfig food)
    {
        tempOrder = Instantiate(orderUiPrefab, orderContent);
        tempOrder.GetComponentInChildren<Image>().sprite = food.picIng;
        imageOrderList.Add(new NewMessageImg(food, tempOrder));
        AddNewMessageOrder(food);
    }

    /*int countImage;
    public void RemoveFinishedOrderInBar(SOFoodConfig food)
    {
        for (countImage = imageOrderList.Count - 1; countImage >= 0; countImage--)
            if (imageOrderList[countImage].rawObj.GetComponentInChildren<Image>().sprite == food.picIng)
            {
                Destroy(imageOrderList[countImage].rawObj.gameObject);
                imageOrderList.RemoveAt(countImage);
                break;
            }
    }*/

    public void BlurryFinishedOrderInBar(SOFoodConfig food, int total)
    {
        foreach (NewMessageImg img in imageOrderList)
            if (img.food == food)
            {
                sfxDoneOrder.Play();
                img.isChanged = true;

                if (img.rawObj != null)
                    ChangeAlphaFoodImage(img);

                finishMes++;
                amountMesTxt.text = $"Bạn hoàn thành {finishMes}/{total} đơn <br> trong ngày.";
                break;
            }
    }

    public void ChangeAlphaFoodImage(NewMessageImg bg)
    {
        bg.rawObj.GetComponent<RawImage>().color = Color.gray;

        tmpColor = bg.rawObj.GetComponentInChildren<Image>().color;
        tmpColor.a = 0.4f;
        bg.rawObj.GetComponentInChildren<Image>().color = tmpColor;

        bg.isChanged = false;
    }
    #endregion

    #region Phone Tab
    public void CheckAmountOfMessagesInADay(int total)
    {
        amountMesTxt.text = $"Bạn có 0/{total} đơn <br> trong ngày.";
    }

    void AddNewMessageOrder(SOFoodConfig food)
    {
        samplBubble = Instantiate(bubblePrefab, mesContent);
        samplBubble.GetComponentInChildren<TMP_Text>().text = $"Bán cho 1 cái {food.nameIng}!";
    }
    #endregion
}
