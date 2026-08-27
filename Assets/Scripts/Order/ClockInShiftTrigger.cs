using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInShiftTrigger : MonoBehaviour
{
    [SerializeField] EmployeeShiftManager shiftMane;

    void Start()
    {
        if (shiftMane == null)
            shiftMane = FindAnyObjectByType<EmployeeShiftManager>();
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
            if (player.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
            {
                shiftMane.IsPlayerInShift = true;
                GetComponent<ClockInShiftTrigger>().enabled = false;
            }
    }
}
