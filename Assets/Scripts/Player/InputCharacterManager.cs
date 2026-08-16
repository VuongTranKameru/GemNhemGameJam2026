using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCharacterManager : MonoBehaviour
{
    [Header("Input")]
    PlayerInputAction inputP;

    public PlayerInputAction.MainActions PlayerInput { get { return inputP.Main; } }

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
        
    }
}