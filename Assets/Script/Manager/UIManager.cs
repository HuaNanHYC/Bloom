using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }
    public bool if_Teach1;//�жϸ��ؿ��Ľ̳̣���ֻ֤��һ�ν̳�
    public bool if_Teach2;
    public bool if_Teach3;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    public void LoadScene(string targetSceneName)//������ת
    {
        SceneManageSystem.Instance.GoToFigureScene(targetSceneName);
    }
    public void SaveGame()//������Ϸ
    {
        InventoryManager.Instance.SavePlayerData();
    }
    public void ClearData()//����浵
    {
        InventoryManager.Instance.ClearPlayerData();
    }

    public void QuitTheGame()//�˳���Ϸ
    {
        Application.Quit();
    }
}
