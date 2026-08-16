using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    [Header("Rig")]
    [SerializeField] Rigidbody2D rigP;
    [SerializeField] InputCharacterManager inputManager;
    float inputX, inputY;

    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float runSpd;

    void Start()
    {
        
    }

    void Update()
    {
        TopdownMovement();
    }

    #region Input 
    void TopdownMovement()
    {
        inputX = inputManager.PlayerInput.Movement.ReadValue<Vector2>().x;
        inputY = inputManager.PlayerInput.Movement.ReadValue<Vector2>().y;

        if (inputX % 1 == 0 && inputY % 1 == 0)
            rigP.velocity = new Vector2(inputX * speed, inputY * speed);
    }
    #endregion
}