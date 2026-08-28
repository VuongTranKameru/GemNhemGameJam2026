using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInShiftTrigger : MonoBehaviour
{
    [SerializeField] EmployeeShiftManager shiftMane;
    [SerializeField] GameObject ost;

    void Start()
    {
        if (shiftMane == null)
            shiftMane = FindAnyObjectByType<EmployeeShiftManager>();
    }

    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.CompareTag("Player"))
            if (player.GetComponentInParent<InputCharacterManager>().PlayerInput.Interact.IsPressed())
                AutomaticInShift();
    }

    public void AutomaticInShift()
    {
        shiftMane.IsPlayerInShift = true;
        ost.SetActive(true);
        GetComponent<ClockInShiftTrigger>().enabled = false;
    }
}
