using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    [SerializeField]
    [Header("�ӵ���ȡ˳���index���������ã�")]
    private int bulletIndexShoot;//���ʱ��index
    private int bulletIndexBeforeShoot;//���ǰ�Ŷ��е�index

    public bool if_BattleCanStart;//�Ƿ�ʼս��
    public bool if_ShootStart;//�Ƿ���Կ�ʼ������������ӵ����������������֮��
    [Header("Ŀǰ����(��������)")]
    public Enemy currentEnemy;
    [Header("ս��ҳ��")]
    public GameObject battlePage;//���ڶ���֮��İɡ�����ʱû��
    public Button playerShootButton;//�����ʱ������ҵ�������ǹ�İ�ť
    private void Start()
    {
        bulletIndexBeforeShoot = 0;
        currentEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();//�ҵ�����������
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
        if(bullets.Count > 8)//��֤����ֻ��8������Ϊ��ʱ���16����bug
        {
            bullets.RemoveRange(8, bullets.Count);
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
            switch(bullets[bulletIndexBeforeShoot].ID)
            {
                #region ��ֵ� 
                case 10002:
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
        if_ShootStart = true;
        StartShootAnim();
    }
    
    public void StartShootAnim()//��ʼ�������Һ͵��˵Ķ�������
    {
        if (ShootEnd()) return;
        if (if_PlayerShoot)
        {
            playerShootButton.gameObject.SetActive(true);
            //�����ť�󲥷ſ�ǹ����
        }
        else
        {
            //�����Զ���ǹ��ǹ�Ķ���
            JudegeShoot();
        }
    }

    [Header("�ж�����˭�ȿ�ǹ���������ã�")]
    public bool if_PlayerShoot;
    public void SetIfPlayerFirst(bool setting)//������ť����Ҿ����Ⱥ�ǹ˳��
    {
        if_PlayerShoot = setting;
    }
    public void JudegeShoot()//��ʼ������ж��ӵ�����
    {
        //�ж��ӵ�

        #region ���ӵ��ж�
        if (!if_haveJudgedFirstShootFailed)
        {
            if (TheFirstShootFailed())
            {
                if_PlayerShoot = !if_PlayerShoot;
                StartShootAnim();//�ٴο�ʼ����ж�
                return;
            }
        }
        #endregion

        #region �������ж�
        if (ShootTwice())
        {
            StartShootAnim();
            return;
        }
        #endregion

        #region ��ͨ�ӵ��ж�
        Shoot();
        bulletIndexShoot++;
        StartShootAnim();
        #endregion
    }
    public void Shoot()//����Ŀǰ˭����ж��ӵ���˭����˺�
    {
        //����˺�
        float damage = bullets[bulletIndexShoot].actualDamage;
        if (if_PlayerShoot)
        {
            InventoryManager.Instance.PlayerGetHurt(damage);
            if_PlayerShoot = false;
            return;
        }
        else if (!if_PlayerShoot)
        {
            currentEnemy.EnemyGetHurt(damage);
            if_PlayerShoot = true;
            return;
        }

    }
    public bool ShootEnd()
    {
        if(bulletIndexShoot>bullets.Count-1)
        {
            if_haveJudgedFirstShootFailed = false;//��һ�������ǹ�жϻָ�
            bulletIndexShoot = 0;
            //�����Ĵ���







            return true;
        }
        return false;
    }
    #region ��Ӱ����е��ӵ�����

    public void NotTheFirst()//��ֵ�
    {
        RandomBulletQueue();//�Ǿ���������
    }


    #endregion

    #region ��Ӱ����е��ӵ�����
    public bool if_haveJudgedFirstShootFailed;//�ж��Ƿ��Ѿ��ж��˵�һ�����ʧЧ
    public bool TheFirstShootFailed()//���ӵ�
    {
        if(bullets.Find(x=>x.ID==10003))
        {
            if_haveJudgedFirstShootFailed = true;
            return true;
        }
        return false;
    }
    public bool ShootTwice()//������
    {
        if (bullets[bulletIndexShoot].ID==10004)
        {
            Shoot();
            bulletIndexShoot++;
            if_PlayerShoot = !if_PlayerShoot;//����ͬһ������
            Shoot();
            bulletIndexShoot++;
            return true;
        }
        return false;
    }
    #endregion
}
