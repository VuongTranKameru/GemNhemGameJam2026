using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    SaveManager saveMane;
    [SerializeField] GameObject annouceBoard;

    [Header("Audio")]
    [SerializeField] AudioSource ostCollector;
    [SerializeField] AudioSource[] sfxCollector;
    [SerializeField] Slider sliderMusic, sliderSfx;

    [Header("Screen Resolution")]
    [SerializeField] Toggle fullscreenTog;
    [SerializeField] TMP_InputField widthInput;
    [SerializeField] TMP_InputField heightInput;
    string correctedFieldTxt;
    int intoNum, wNum, hNum;
    bool isFullscreen;

    void Awake()
    {
        if (saveMane == null)
            saveMane = FindAnyObjectByType<SaveManager>();

        if (ostCollector == null)
            ostCollector = FindAnyObjectByType<ClockInShiftTrigger>()?.GetOST.GetComponent<AudioSource>();

        if (sfxCollector[0] == null || sfxCollector[1] == null)
        {
            sfxCollector[0] = FindAnyObjectByType<InputCharacterManager>()?.GetComponent<AudioSource>();
            sfxCollector[1] = FindAnyObjectByType<CustomerOrderManager>()?.GetComponent<AudioSource>();
        }

        ChangeSetting(); //for setting in main menu
    }

    private void Start()
    {
        EnableSettingMenu();
    }

    public void EnableSettingMenu() //alwasy put on the open setting button
    {
        sliderMusic.value = saveMane.MusicVolume;
        sliderSfx.value = saveMane.SfxVolume;
        fullscreenTog.isOn = saveMane.FullScreenMod;

        widthInput.text = Screen.width.ToString();
        heightInput.text = Screen.height.ToString();
    }

    public void SaveTheChange()
    {
        saveMane.MusicVolume = sliderMusic.value;
        saveMane.SfxVolume = sliderSfx.value;

        saveMane.FullScreenMod = isFullscreen;
        ConvertFromStringToInt();
        saveMane.WidthScreenReso = wNum;
        saveMane.HeightScreenReso = hNum;

        StartCoroutine(AnnoucementPopup());
    }

    public void DisableSettingMenu()
    {
        ChangeSetting();
    }

    void ChangeSetting()
    {
        ChangeMusicVolumeSetting(saveMane.MusicVolume);
        ChangeSfxVolumeSetting(saveMane.SfxVolume);

        ConvertFromStringToInt();
        //if (wNum != saveMane.WidthScreenReso || hNum != saveMane.HeightScreenReso)
        ChangeResolution(saveMane.WidthScreenReso, saveMane.HeightScreenReso);
        
        if (isFullscreen != saveMane.FullScreenMod)
            fullscreenTog.isOn = saveMane.FullScreenMod;
    }

    IEnumerator AnnoucementPopup()
    {
        annouceBoard.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        annouceBoard.SetActive(false);
    }

    #region Music Setting
    public void ValueOfMusicVolume(float volume)
    {
        ChangeMusicVolumeSetting(volume);
    }

    public void ValueOfSfxVolume(float volume)
    {
        ChangeSfxVolumeSetting(volume);
    }

    void ChangeMusicVolumeSetting(float newOst)
    {
        if (ostCollector != null)
            ostCollector.outputAudioMixerGroup.audioMixer.SetFloat("MusicEP", newOst);
    }

    void ChangeSfxVolumeSetting(float newSfx)
    {
        if (sfxCollector[0] != null && sfxCollector[1] != null)
            sfxCollector[0].outputAudioMixerGroup.audioMixer.SetFloat("SfxEP", newSfx);
    }
    #endregion

    #region Screen Resolution Setting
    public void PreviewFullscreenMode(bool checkBox)
    {
        ChangeFullscreenMode(checkBox);
        isFullscreen = checkBox;
    }

    void ChangeFullscreenMode(bool fullscr)
    {
        if (fullscr)
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        else Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void TryResolution(string inp)
    {
        int.TryParse(inp, out intoNum);
        if (intoNum < 400 || inp == "")
            correctedFieldTxt = "400";
        else correctedFieldTxt = inp;
    }

    public void CatchResolution(TMP_InputField field)
    {
        field.text = correctedFieldTxt;
    }

    void ConvertFromStringToInt()
    {
        int.TryParse(widthInput.text, out wNum);
        int.TryParse(heightInput.text, out hNum);
    }

    public void PreviewNewResolution()
    {
        ConvertFromStringToInt();
        ChangeResolution(wNum, hNum);
    }

    void ChangeResolution(int wit, int hei)
    {
        Screen.SetResolution(wit, hei, Screen.fullScreenMode);
    }
    #endregion
}
