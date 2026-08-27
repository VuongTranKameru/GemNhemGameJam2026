using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    [Header("Rig")]
    [SerializeField] Rigidbody2D rigging;
    [SerializeField] InputCharacterManager inputManager;
    float inputX, inputY;

    [Header("Interact")]
    [SerializeField] GameObject interactCollider;
    [SerializeField] GameObject holdItem, itemPrefab;
    Action<InputAction.CallbackContext> cookStarted, cookPressed;

    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float runSpd;

    private void Start()
    {
        CookDelegateHandler();
    }

    void Update()
    {
        TopdownMovement();
    }

    #region Outside ref
    public void EnableStartingCircleWhileCooking() //use by InteractablePlayer.cs
    {
        inputManager.PlayerInput.Cook.started += cookStarted;
    }

    public void EnableLoadingCircleWhileCooking() //use by InteractablePlayer.cs
    {
        inputManager.PlayerInput.Cook.performed += cookPressed;
    }

    public void DisableCircleWhileCooking() //use by InteractablePlayer.cs
    {
        inputManager.PlayerInput.Cook.started -= cookStarted;
        inputManager.PlayerInput.Cook.performed -= cookPressed;
    }
    #endregion

    #region Input 
    void TopdownMovement()
    {
        inputX = inputManager.PlayerInput.Movement.ReadValue<Vector2>().x;
        inputY = inputManager.PlayerInput.Movement.ReadValue<Vector2>().y;

        if (inputManager.PlayerInput.Run.inProgress)
            rigging.velocity = new Vector2(inputX * runSpd, inputY * runSpd);
        else rigging.velocity = new Vector2(inputX * speed, inputY * speed);
        FacingInteract();
    }

    void ItemHoldOnHead()
    {
        if (holdItem.activeInHierarchy)
            if (inputManager.PlayerInput.Interact.triggered)
                Instantiate(itemPrefab);
    }
    #endregion

    #region Animation
    void FacingInteract()
    {
        if (inputY == 1)
            interactCollider.transform.rotation = Quaternion.Euler(0, 0, 180);
        else if (inputY == -1)
            interactCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (inputX == 1)
            interactCollider.transform.rotation = Quaternion.Euler(0, 0, 90);
        else if (inputX == -1)
            interactCollider.transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    void CookDelegateHandler()
    {
        inputManager.PlayerInput.Cook.canceled += context => CookingAnim(Color.white);
        cookStarted = context => CookingAnim(Color.magenta);
        cookPressed = context => CookingAnim(Color.white);
    }

    void CookingAnim(Color co)
    {
        GetComponent<SpriteRenderer>().color = co;
    }
    #endregion
}