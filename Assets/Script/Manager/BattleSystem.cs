using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    [Header("�ӵ���ȡ˳���index")]
    private int bulletIndexShoot;//���ʱ��index
    private int bulletIndexBeforeShoot;//���ǰ�Ŷ��е�index

    public bool if_BattleCanStart;//�Ƿ�ʼս��
    public bool if_ShootStart;//�Ƿ���Կ�ʼ������������ӵ����������������֮��

    [Header("ս��ҳ��")]
    public GameObject battlePage;//���ڶ���֮��İɡ�����ʱû��
    private void Start()
    {
        bulletIndexBeforeShoot = 0;
    }
    private void Update()
    {
        if (BulletManager.Instance.loadedBulletList.Count > 0 && !if_BattleCanStart) if_BattleCanStart = true;//���û��ʼս�����������������ÿ�ʼս��
    }
    public List<Bullet> bullets=new List<Bullet>();//����ӵ����У����óɹ��з���鿴
    public void StartTheBattle()//������ʼ��ť
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
        bulletIndexBeforeShoot = 0;//����ҲҪ����һ��
    }
    public void JudgeListEnable()//�ж϶����Ƿ������Ϊս�����п�������Ҫ����Ӱ����е��ӵ�
    {
        while(bulletIndexBeforeShoot < bullets.Count)
        {
            switch(bulletIndexBeforeShoot)
            {
                #region ��ֵ� 
                case 1002:
                    if (bulletIndexBeforeShoot != 0)
                    {
                        bulletIndexBeforeShoot++; 
                        break;
                    }
                    NotTheFirst();
                    JudgeListEnable();
                    break;
                #endregion


                default:
                    bulletIndexBeforeShoot++;
                    break;
            }
        }
        StartShoot();
    }
    public void StartShoot()//���Կ�ʼ���
    {
        if_ShootStart = true;
    }

    [Header("�ж�����˭�ȿ�ǹ")]
    public bool if_PlayerShoot;
    public void JudegeShoot()//��ʼ������ж��ӵ�����
    {

    }

    #region ��Ӱ����е��ӵ�����

    public void NotTheFirst()//��ֵ�
    {
        RandomBulletQueue();//�Ǿ���������
    }


    #endregion
}
