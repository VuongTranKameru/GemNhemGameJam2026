using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    static DialogueManager instance;

    [Header("Dialogue UI")]
    [SerializeField] GameObject panel;
    [SerializeField] TMP_Text txtDialg, nameChar;
    [SerializeField] Image avaChar;
    [SerializeField] float speedTxt;
    bool isDoneALine;
    int countLine;

    [Header("NPC Controller")]
    CharacterLines[] charLines;
    bool isEndDialog;

    [Header("Scene")]
    [SerializeField] UnityEvent changeSceneEvent;

    public CharacterLines[] PutLinesOnFrame
    {
        set
        {
            charLines = value;
            isEndDialog = false;
            SetUpTheFrame(true);
            panel.GetComponent<Selectable>().Select();
            FindAnyObjectByType<EventSystem>().firstSelectedGameObject = panel.GetComponent<Selectable>().gameObject;
            SpeakOutTheLine();
        }
    }

    public bool IsEndTheDialogueLines
    {
        get => isEndDialog;
        set => isEndDialog = value;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    #region Input from DialogueChar and EventTrigger
    public void SpeakOutTheLine()
    {
        if (isDoneALine)
        {
            if (countLine >= charLines.Length)
                AllowPlayerToMove();
            else 
            {
                StartCoroutine(TextSpeedTyping()); //dialogueTxt.text = scriptOfDiag[countLine].hoiThoai;
                if (countLine < charLines.Length)
                    countLine++;
            }
        }
    }
    #endregion

    #region Dialogue Manager
    void SetUpTheFrame(bool checkActive)
    {
        txtDialg.text = "";
        nameChar.text = "";
        countLine = 0;
        isDoneALine = true;
        isEndDialog = false;
        panel.SetActive(checkActive);
    }

    IEnumerator TextSpeedTyping()
    {
        isDoneALine = false;
        nameChar.text = charLines[countLine].character;
        txtDialg.text = "";
        txtDialg.text = charLines[countLine].line;
        txtDialg.maxVisibleCharacters = 0;
        if (charLines[countLine].ava != null)
        {
            avaChar.sprite = charLines[countLine].ava;
            if (!avaChar.gameObject.activeInHierarchy)
                avaChar.gameObject.SetActive(true);
        }
        else avaChar.gameObject.SetActive(false);

        foreach (char letter in charLines[countLine].line.ToCharArray())
        {   
            txtDialg.maxVisibleCharacters++;
            yield return new WaitForSeconds(speedTxt);
        }

        isDoneALine = true;
    }
    #endregion

    #region Prepare and sent to Player Input
    public void AllowPlayerToMove()
    {
        isEndDialog = true;
        panel.SetActive(false);
        avaChar.gameObject.SetActive(false);
        changeSceneEvent.Invoke();
    }
    #endregion
}
