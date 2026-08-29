using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialEventGM : GameManager
{
    [Header("Dialogue")]
    DialogueManager diaMane;
    [SerializeField] UnityEvent mayOverride;
    [SerializeField] SODialogueConfig norma, gud;

    protected override void GameOverThePlayer()
    {
        mayOverride.Invoke();
        base.GameOverThePlayer();
    }

    public void DeteminateWinEvent()
    {
        if (levelSetting.name.Contains("DB"))
        {
            GetComponent<VariantScenes>().ChangeScene("Stage3-SPED");
            //LoadDialogueDeteminateScene(gud);
        }
        else LoadDialogueDeteminateScene(norma);
    }

    public void OverrideLevelSetting(SOLevelConfig lv)
    {
        levelSetting = lv;
        checkOrderList = new();
        SettingLevel();
    }

    public void OverrideIsGameOver()
    {
        shiftMane.TurnOffIsGameOver = false;
    }

    public void LoadDialogueDeteminateScene(SODialogueConfig dialog)
    {
        diaMane = FindAnyObjectByType<DialogueManager>();
        diaMane.PutLinesOnFrame = dialog.NormalDialogueLines;
    }
}
