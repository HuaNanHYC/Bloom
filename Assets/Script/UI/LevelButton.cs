using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelButton : MonoBehaviour
{
    //public int levelId;
    //public string figureScene;
    private Button button;
    public bool if_Last;
    [Header("���������ԣ���������ȡ1-8")]
    public int index=1;
    void Start()
    {
        button = GetComponent<Button>();
        ButtonOnClick();

    }
    public void SetCurrentLevel()//��ť��������õ�ǰ�ؿ�
    {
        if (if_Last) return;
        LevelManager.Instance.SetCurrentLevel(LevelManager.Instance.currentLevelId + index);
    }
    public void StartGame()//��ť�����ʼ��Ϸ����ת����
    {
        LevelManager.Instance.DialogueAfterBlack();
    }
    public void ButtonOnClick()
    {
        button.onClick.AddListener(()=>SetCurrentLevel());
        button.onClick.AddListener(()=>StartGame());
    }

}
