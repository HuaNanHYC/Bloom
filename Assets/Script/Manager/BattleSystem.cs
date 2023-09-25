using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    [Header("�ӵ���ȡ˳���index")]
    private int bulletIndex;

    public bool if_BattleCanStart;//�Ƿ�ʼս��
    public bool if_ShootStart;//�Ƿ���Կ�ʼ������������ӵ����������������֮��

    [Header("ս��ҳ��")]
    public GameObject battlePage;//���ڶ���֮��İɡ�����ʱû��
    private void Start()
    {
        bulletIndex = 0;
    }
    private void Update()
    {
        if (BulletManager.Instance.loadedBulletList.Count > 0 && !if_BattleCanStart) if_BattleCanStart = true;//���û��ʼս�����������������ÿ�ʼս��
    }
    public List<Bullet> bullets=new List<Bullet>();//����ӵ����У����óɹ��з���鿴
    public void StartTheBattle()//���Ը�ֵ����ť
    {
        RandomBulletQueue();
        JudgeListEnable();
    }
    public void RandomBulletQueue()//���ѡȡ�ӵ�
    {
        bullets.Clear();//������У�Ȼ���ٴ����
        //�������һ���ӵ�����������Ϊ��һ��
        int listLength = BulletManager.Instance.loadedBulletList.Count;
        int index = Random.Range(0, listLength);
        for (int i = index; i < listLength * 2; i++)
        {
            bullets.Add(BulletManager.Instance.loadedBulletList[i%listLength]);
            if (i % listLength == index - 1) break;//�ֵ�ͬ��Ԫ�ص�ǰһ����ͣ��
        }
        InitializeAllBullets();//���������õ��ӵ�����

    }
    public void InitializeAllBullets()//�������ӵ��˺������֮���
    {
        if (bullets == null) return;
        for(int i=0; i<bullets.Count; i++)
        {
            bullets[i].InitializeBullet();
        }
        bulletIndex = 0;//����ҲҪ����һ��
    }
    public void JudgeListEnable()//�ж϶����Ƿ������Ϊս�����п���
    {
        while(bulletIndex < bullets.Count)
        {
            switch(bulletIndex)
            {
                #region �ǵ�һ���ӵ����ж�
                case 1002:
                    if (bulletIndex != 0)
                    {
                        bulletIndex++; 
                        break;
                    }
                    NotTheFirst();
                    JudgeListEnable();
                    break;
                #endregion

                #region ��һ���ӵ��˺��������ж�
                case 1003:
                    DoubleTheNextBullet();
                    break;

                #endregion







                default:
                    bulletIndex++;
                    break;
            }
        }
        StartShoot();
    }
    public void StartShoot()//���Կ�ʼ���
    {
        if_ShootStart = true;

    }


    #region �ӵ�����

    public void NotTheFirst()//������Ϊ��һ��
    {
        RandomBulletQueue();//�Ǿ���������
    }
    private void DoubleTheNextBullet()//��һ���ӵ������˺�
    {
        bullets[bulletIndex + 1].actualDamege *= 2;
    }


    #endregion
}
