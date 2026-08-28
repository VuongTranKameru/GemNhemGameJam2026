using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EmployeeShiftManager : MonoBehaviour
{
    static EmployeeShiftManager instance;

    [Header("Setting")]
    [SerializeField] float workTime; //second
    int minute, second;
    bool isTimeRunning, isGameOver;

    [Header("UI")]
    [SerializeField] TMP_Text countdownTxt;
    [SerializeField] GameObject gameOverScreen;

    public float GetWorkTime { get => Mathf.FloorToInt(workTime); }
    public float SetWorkTime { set => workTime = value; }
    public bool IsPlayerInShift {
        get => isTimeRunning;
        set => isTimeRunning = true; 
    }

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        CalculatePerMinutePerSec();
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Update()
    {
        if (isTimeRunning)
        {
            if (workTime > 0)
                CountdownShiftTime();
            else EndOfShift();
        }
    }

    public bool IsGameOverScene() //use in GameManager.cs
    {
        if (isGameOver)
            gameOverScreen.SetActive(true);
        return isGameOver;
    }

    void CalculatePerMinutePerSec()
    {
        second = Mathf.FloorToInt(workTime % 60);
        minute = Mathf.FloorToInt(workTime / 60);
        countdownTxt.text = string.Format("{0:00}:{1:00}", minute, second);
    }

    void CountdownShiftTime()
    {
        workTime -= Time.deltaTime;
        CalculatePerMinutePerSec();
    }

    void EndOfShift()
    {
        isGameOver = true;
        countdownTxt.text = "00:00";
        isTimeRunning = false;
    }
}
