using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacterManager : MonoBehaviour
{
    [Header("Input")]
    PlayerInputAction inputP;

    [Header("Menu Input")]
    [SerializeField] MenuManager menu;
    [SerializeField] PauseManager pause;

    [Header("To Dialogue Controller throught InteractablePlayer")]
    bool isTalkwNPC;

    public PlayerInputAction.MainActions PlayerInput { get { return inputP.Main; } }
    public PlayerInputAction.GeneralActions MenuInput { get { return inputP.General; } }

    public bool IsTalkWithNpcRn
    {
        get { return isTalkwNPC; }
        set
        {
            isTalkwNPC = value;

            if (!isTalkwNPC)
                inputP.Main.Enable();
        }
    }

    void Awake()
    {
        if (inputP == null)
            inputP = new();

        if (menu == null || pause == null)
        {
            menu = FindAnyObjectByType<MenuManager>();
            pause = FindAnyObjectByType<PauseManager>();
        }

        menu.gameObject.SetActive(false);
        pause.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        inputP.Enable();
    }

    private void OnDisable()
    {
        inputP.Disable();
    }

    void Update()
    {
        OpenMenu();
        OpenPause();
        StandStillWhileTalking();
    }

    #region Menu Input
    void OpenMenu()
    {
        if (inputP.General.OpenMenu.triggered)
        {
            if (!menu.gameObject.activeInHierarchy)
            {
                inputP.Main.Disable();
                menu.gameObject.SetActive(true);
            }
            else
            {
                inputP.Main.Enable();
                menu.gameObject.SetActive(false);
            }
        }
    }

    void OpenPause()
    {
        if (inputP.General.Pause.triggered)
        {
            if (!pause.gameObject.activeInHierarchy)
            {
                inputP.Main.Disable();
                inputP.General.OpenMenu.Disable();
                pause.gameObject.SetActive(true);
            }
            else
            {
                inputP.Main.Enable();
                inputP.General.OpenMenu.Enable();
                pause.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region Outside Input
    void StandStillWhileTalking()
    {
        if (isTalkwNPC && inputP.Main.Interact.IsPressed())
            inputP.Main.Disable();
    }

    public void EnablePlayerInput()
    {
        inputP.Main.Enable();
    }

    public void DisablePlayerInput()
    {
        inputP.Main.Disable();
    }
    #endregion
}