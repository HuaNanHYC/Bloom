using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePage : MonoBehaviour
{
    public BattleSystem battleSystem;
    public GameObject startBattle;//��ʼ�İ�ť
    [Header("��ʼ�����������ʾ")]
    public GameObject revolverPage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JudgeCanBattle();
        //JudgeRevolverBulletRotate();
    }
    bool if_show_StartButton=false;//�Ƿ���ʾ��ʼ��ť
    public void JudgeCanBattle()//�ж��Ƿ���Կ�ʼս������ʾ��ʼ��ť
    {
        if (LevelManager.Instance.currentLevelId == 30008 && !if_show_StartButton)
        {
            startBattle.gameObject.SetActive(true);
            if_show_StartButton = true;
        }
        if(battleSystem.if_BattleCanStart&& !if_show_StartButton)//ֻ������ʾһ��
        {
            startBattle.gameObject.SetActive(true);
            if_show_StartButton = true;
        }
    }
    public void JudgeRevolverBulletRotate()//�ж��Ƿ���ת����,10.7����
    {
        if(battleSystem.if_BattleCanStart&&!battleSystem.if_ShootStart)
        {
            //��������ת����
        }
        else if(battleSystem.if_ShootStart)//��ת��������ʼ���
        {
            //���������ֹͣ��ת�Ķ���

            return;
        }
    }
}
