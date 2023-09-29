using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelId;
    public string figureScene;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        ButtonOnClick();

    }
    public void SetCurrentLevel()//��ť��������õ�ǰ�ؿ�
    {
        LevelManager.Instance.SetCurrentLevel(levelId);
    }
    public void StartGame()//��ť�����ʼ��Ϸ����ת����
    {
        SceneManageSystem.Instance.GoToFigureScene(figureScene);
    }
    public void ButtonOnClick()
    {
        button.onClick.AddListener(()=>SetCurrentLevel());
        button.onClick.AddListener(()=>StartGame());
    }

}
