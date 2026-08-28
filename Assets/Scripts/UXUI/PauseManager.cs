using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject menuBeforeIntruct;

    public void StartMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableInstructionScene()
    {
        menuBeforeIntruct.SetActive(false);
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Additive);
    }

    public void DisableInstructionScene()
    {
        menuBeforeIntruct.SetActive(true);
        SceneManager.UnloadSceneAsync("HowToPlay", UnloadSceneOptions.None);
    }
}
