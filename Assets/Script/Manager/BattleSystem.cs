using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    [Header("�ӵ���ȡ˳���index")]
    private int bulletIndex;

    public bool if_BattleStart;//�Ƿ�ʼս��

    [Header("ս��ҳ��")]
    public GameObject battlePage;//���ڶ���֮��İɡ�����ʱû��

    private void Update()
    {
        if (BulletManager.Instance.loadedBulletDictionary.Count>0) if_BattleStart = true;
    }
    public void FirstRamdom()//��һ������ӵ�
    {

    }


}
