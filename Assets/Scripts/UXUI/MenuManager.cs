using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("ButtonTab")]
    GameObject[] tabLists;
    [SerializeField] Button cTab, iTab;
    [SerializeField] GameObject bookPanel, phonePanel;
    int numOpenTab;
    bool isChangeTab;

    [Header("KeyButton")]
    [SerializeField] InputCharacterManager input;

    private void Awake()
    {
        if (tabLists == null) //khai bao stt cac tab
            tabLists = new GameObject[2] { bookPanel, phonePanel };

        if (input == null)
            input = FindAnyObjectByType<InputCharacterManager>();
    }

    void Start()
    {
        ClickTabWithButton(); //first screen seen when open backpack is inventory
    }

    void Update()
    {
        ClickKeyButtonSwitchTab();

        if (isChangeTab)
            ClickTabWithButton();
    }

    public void CheckTabChanging()
    {
        isChangeTab = true;
    }

    public void OpenBookTab(bool check)
    {
        bookPanel.SetActive(check);
        cTab.GetComponent<Image>().enabled = !check;
        phonePanel.SetActive(!check);
        iTab.GetComponent<Image>().enabled = check;
        CheckTabChanging();
    }

    void ConnectTabWithPanel(GameObject panel, Button tab)
    {
        if (panel.activeInHierarchy)
        {
            tab.interactable = false;
            for (int i = 0; i < tabLists.Length; i++)
            {
                if (tabLists[i] == panel)
                {
                    numOpenTab = i;
                    break;
                }
            }
        }
        else tab.interactable = true;
    }

    void ClickTabWithButton()
    {
        ConnectTabWithPanel(bookPanel, cTab);
        ConnectTabWithPanel(phonePanel, iTab);
        isChangeTab = false;
    }

    void ClickKeyButtonSwitchTab()
    {
        if (input.MenuInput.UpTab.triggered)
            OpenBookTab(true);
        else if (input.MenuInput.DownTab.triggered)
            OpenBookTab(false);

    }
}
