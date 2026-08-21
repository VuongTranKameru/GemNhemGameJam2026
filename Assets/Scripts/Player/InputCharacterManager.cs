using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacterManager : MonoBehaviour
{
    [Header("Input")]
    PlayerInputAction inputP;

    [Header("To Dialogue Controller throught InteractablePlayer")]
    bool isTalkwNPC;

    public PlayerInputAction.MainActions PlayerInput { get { return inputP.Main; } }

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
        StandStillWhileTalking();
    }

    #region Outside Input
    void StandStillWhileTalking()
    {
        if (isTalkwNPC && inputP.Main.Interact.IsPressed())
            inputP.Main.Disable();
    }
    #endregion
}