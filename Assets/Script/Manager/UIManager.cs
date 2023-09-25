using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public UIManager Intance { get => instance; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance.gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void StartTheGame(string targetSceneName)
    {
        //��ʼ��Ϸ��Э��,��ֱ��ת
        SceneManager.LoadScene(targetSceneName);
    }


    public void QuitTheGame()
    {
        Application.Quit();
    }
}
