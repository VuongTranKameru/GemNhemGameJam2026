using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    static SaveManager instance;

    [Header("Audio Setting")]
    float musicVo, sfxVo;

    [Header("Screen Setting")]
    bool fScreen, firstCheckScreenReso;
    int wScreen, hScreen;

    public float MusicVolume
    {
        get => musicVo;
        set
        {
            PlayerPrefs.SetFloat("Music_Volume", value);
            musicVo = value;
        }
    }

    public float SfxVolume
    {
        get => sfxVo;
        set
        {
            PlayerPrefs.SetFloat("Sfx_Volume", value);
            sfxVo = value;
        }
    }

    public bool FullScreenMod
    {
        get => fScreen;
        set => fScreen = value;
    }

    public int WidthScreenReso
    {
        get => wScreen;
        set => wScreen = value;
    }

    public int HeightScreenReso
    {
        get => hScreen;
        set => hScreen = value;
    }

    void Awake()
    {
        if (instance == null)
            instance = this; 
        else if (instance != this)
            Destroy(gameObject);

        if (!firstCheckScreenReso)
        {
            WidthScreenReso = Screen.width;
            HeightScreenReso = Screen.height;
            if (Screen.fullScreenMode != FullScreenMode.Windowed)
                fScreen = true;

            firstCheckScreenReso = true;
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(instance);

        if (!PlayerPrefs.HasKey("Music_Volume"))
            PlayerPrefs.SetFloat("Music_Volume", -20);

        musicVo = PlayerPrefs.GetFloat("Music_Volume");
        sfxVo = PlayerPrefs.GetFloat("Sfx_Volume");
    }
}
