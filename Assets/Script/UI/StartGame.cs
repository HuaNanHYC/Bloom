using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGame : MonoBehaviour
{
    Button button;
    public bool if_JumpVideo;
    private void Start()
    {
        button = GetComponent<Button>();
        ButtonOnClick();
    }
    public void StartTheGame()
    {
        LevelManager.Instance.currentLevelId = 30000;
        LevelManager.Instance.InitializeAllIndexInDialogueDic();//����
        LevelManager.Instance.StartVideoPlay = false;
        if(if_JumpVideo)
        {
            LevelManager.Instance.NextLevel();
            return;
        }
        UIManager.Instance.LoadScene("StartAndEnd");
    }
    public void ContinueTheGame()
    {
        LevelManager.Instance.LoadTheGame();
        if (LevelManager.Instance.currentLevelId <= 30000) return;
        LevelManager.Instance.InitializeAllIndexInDialogueDic();//����
        if (LevelManager.Instance.currentLevelId == 30001)
        {
            LevelManager.Instance.currentLevelId = 30000;
            LevelManager.Instance.StartVideoPlay = false;
            StartTheGame();
        }
        else
        {
            LevelManager.Instance.StartVideoPlay = true;
            LevelManager.Instance.DialogueAfterBlack();
        }
    }
    public void ButtonOnClick()
    {
    }
}
