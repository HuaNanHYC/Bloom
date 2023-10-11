using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    [Header("���˲��Ŷ����ٶ�")]
    public float beforeActionInterval;
    public float actionToReadyInterval;
    public float readyToShootInterval;
    public float shootToReadyInterval;
    public float readyToActionInterval;
    public float actionToIdleInterval;
    [Header("��Ҳ��Ŷ����ٶ�")]
    public float beforeActionIntervalPlayer;
    public float actionToReadyIntervalPlayer;
    public float readyToShootIntervalPlayer;
    public float shootToReadyIntervalPlayer;
    public float readyToActionIntervalPlayer;
    public float actionToIdleIntervalPlayer;
    public bool if_Teach1;//�жϸ��ؿ��Ľ̳̣���ֻ֤��һ�ν̳�
    public bool if_Teach2;
    public bool if_Teach3;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SetResolution();
    }


    public void LoadScene(string targetSceneName)//������ת
    {
        SceneManageSystem.Instance.GoToFigureScene(targetSceneName);
    }
    /*public void SaveGame()//������Ϸ
    {
        InventoryManager.Instance.SavePlayerData();
    }
    public void ClearData()//����浵
    {
        InventoryManager.Instance.ClearPlayerData();
    }*/

    public void QuitTheGame()//�˳���Ϸ
    {
        Application.Quit();
        Application.Quit();

        Application.Quit();

    }
    public void SetResolution()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        bool isFullscreen = true; // ����Ը�����Ҫ�������ֵ
        Screen.SetResolution(1920, 1080, isFullscreen);
    }
}
