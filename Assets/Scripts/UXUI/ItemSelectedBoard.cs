using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSelectedBoard : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject checkPanel;
    [SerializeField] GameObject selectPanel;
    [SerializeField] TMP_Text txtChecked, txtSelected;

    private void Awake()
    {
        if (checkPanel.activeInHierarchy || selectPanel.activeInHierarchy)
        {
            checkPanel.SetActive(false);
            selectPanel.SetActive(false);
        }
    }

    public void EnabledShowIngredientInBox(SOIngredientConfig ing, bool enabled)
    {
        txtChecked.text = ing.nameIng;
        checkPanel.SetActive(enabled);
    }

    public void EnabledShowItemHoldByPlayer(SOIngredientConfig ing, bool enabled)
    {
        if (ing != null)
            txtSelected.text = ing.nameIng;
        selectPanel.SetActive(enabled);
    }
}
