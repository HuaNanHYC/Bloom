using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePage : MonoBehaviour
{
    public BattleSystem battleSystem;
    public Button startBattle;//��ʼ�İ�ť

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        JudgeCanBattle();
    }
    public void JudgeCanBattle()//�ж��Ƿ���Կ�ʼս������ʾ��ʼ��ť
    {
        if(battleSystem.if_BattleStart)
        {
            startBattle.gameObject.SetActive(true);
        }
        else startBattle.gameObject.SetActive(false);
    }

}
