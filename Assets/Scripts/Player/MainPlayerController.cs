using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : MonoBehaviour
{
    [Header("Rig")]
    [SerializeField] Rigidbody2D rigging;
    [SerializeField] GameObject interactCollider;
    [SerializeField] InputCharacterManager inputManager;
    float inputX, inputY;

    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float runSpd;

    void Update()
    {
        TopdownMovement();
    }

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
    #endregion
}