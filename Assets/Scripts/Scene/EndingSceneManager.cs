using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingSceneManager : MonoBehaviour
{
    [SerializeField] GameObject endingTxt;
    bool isStartTalking;

    [Header("Dialogue Manager")]
    [SerializeField] GameObject dialogPanel;

    private void Update()
    {
        if (dialogPanel != null)
            EndingTextAppear();
    }

    public void IsStartTalking(bool check)
    {
        isStartTalking = check;
    }

    public void EndingTextAppear()
    {
        if (isStartTalking)
            if (!dialogPanel.activeInHierarchy)
            {
                endingTxt.GetComponent<Selectable>().Select();
                FindAnyObjectByType<EventSystem>().firstSelectedGameObject = endingTxt.gameObject;
                endingTxt.SetActive(true);
                isStartTalking = false;
            }
    }

    #region Scene Variant
    public void CreditScene()
    {
        SceneManager.LoadScene("CreditScene");
    }
    #endregion
}
