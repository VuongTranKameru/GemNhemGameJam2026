using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSelectedBoard : MonoBehaviour
{
    [Header("Cook")]
    [SerializeField] Transform cookContent;
    [SerializeField] GameObject ingredientTag;
    List<GameObject> tagList;
    GameObject tempTag;

    [Header("UI")]
    [SerializeField] GameObject checkPanel;
    [SerializeField] GameObject selectPanel;
    [SerializeField] TMP_Text txtChecked, txtSelected;
    bool isTableCheck;

    public bool IsInteractableTableCheck { get => isTableCheck; }

    private void Awake()
    {
        if (checkPanel.activeInHierarchy || selectPanel.activeInHierarchy)
        {
            checkPanel.SetActive(false);
            selectPanel.SetActive(false);
        }

        tagList = new();
    }

    public void EnabledShowIngredientInBox(SOIngredientConfig ing, bool enabled) //in InteractablePlayer.cs
    {
        txtChecked.text = ing.nameIng;
        checkPanel.SetActive(enabled);
        isTableCheck = enabled;
    }

    public void EnabledShowItemHoldByPlayer(SOIngredientConfig ing, bool enabled) //in HandCheckItemSelect.cs
    {
        if (ing != null)
            txtSelected.text = ing.nameIng;
        selectPanel.SetActive(enabled);
    }

    public void AddIngredientTag(SOIngredientConfig ing) //in CookPlacement.cs
    {
        tempTag = Instantiate(ingredientTag, cookContent);
        tempTag.GetComponentInChildren<TMP_Text>().text = ing.nameIng;
        tagList.Add(tempTag);
    }

    public void DeleteAllIngredientTag() //in CookPlacement.cs
    {
        foreach (GameObject gobj in tagList)
            Destroy(gobj);

        tagList.Clear();
    }
}
