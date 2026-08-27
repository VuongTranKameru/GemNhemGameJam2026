using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
