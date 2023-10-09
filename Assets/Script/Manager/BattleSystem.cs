using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public bool if_TheLastLevel;
    [SerializeField]
    [Header("�ӵ���ȡ˳���index���������ã�")]
    private int bulletIndexShoot;//���ʱ��index
    private int bulletIndexBeforeShoot;//���ǰ�Ŷ��е�index

    public bool if_BattleCanStart;//�Ƿ�ʼս��
    public bool if_ShootStart;//�Ƿ���Կ�ʼ������������ӵ����������������֮��
    public int BulletIndexShoot { get => bulletIndexShoot; set => bulletIndexShoot = value; }
    public int BulletIndexBeforeShoot { get => bulletIndexBeforeShoot; set => bulletIndexBeforeShoot = value; }
    [Header("Ŀǰ����(��������)")]
    public Enemy currentEnemy;
    [Header("ս����ҳ��")]
    public GameObject battlePage;//���ڿ���������ת
    [Header("��ǹ��ť")]
    public Button playerShootButton;//�����ʱ������ҵ�������ǹ�İ�ť
    [Header("����ҳ��")]
    public GameEndPage endPage;//��Ϸ����ҳ��
    public PlayerInfoPage playerInfoPage;
    public EnemyInfoPage enemyInfoPage;
    private void Start()
    {
        bulletIndexBeforeShoot = 0;
        currentEnemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();//�ҵ�����������
        currentEnemy.GetComponent<Enemy>().BattleSystem = this;

        if (LevelManager.Instance.currentLevelId == 30008) LevelManager.Instance.lastLevelJudge = true;
        else LevelManager.Instance.lastLevelJudge = false;
    }
    private void Update()
    {
        if (BulletManager.Instance.loadedBulletList.Count > 0 && !if_BattleCanStart) if_BattleCanStart = true;//���û��ʼս�����������������ÿ�ʼս��
    }
    public List<Bullet> bullets=new List<Bullet>();//����ӵ����У����óɹ��з���鿴
    public void StartTheBattle()//������ʼ��ť
    {
        //��ʼ����������
        InitializeOddList();
        InitializeEvenList();
        InitializePrimeList();
        //*********
        AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_Spin);
        RandomBulletQueue();
        JudgeListEnable();
    }
   
    public void RandomBulletQueue()//���ѡȡ�ӵ�
    {
        //���һ�ص�һ�α�ʧ��
        if(LevelManager.Instance.lastLevelJudge)
        {
            LevelManager.Instance.lastLevelJudge = false;
            bullets.Clear();
            for (int i = 0; i < BulletManager.Instance.loadedBulletList.Count; i++)
            { 
                bullets.Add(BulletManager.Instance.loadedBulletList[5]); 
            }
            Bullet newBullet = BulletManager.Instance.bulletDictionary[10001].GetComponent<Bullet>();
            if (if_PlayerShoot)
            {
                int random = Random.Range(1, 91);
                if (random <= 30) bullets[0] = newBullet;
                else if (random > 30 && random <= 60) bullets[2] = newBullet;
                else if (random > 60) bullets[4]= newBullet;
            }
            else if (!if_PlayerShoot)
            {
                int random = Random.Range(1, 91);
                if (random <= 30) bullets[1] = newBullet;
                else if (random > 30 && random <= 60) bullets[3] = newBullet;
                else if (random > 60) bullets[5] = newBullet;
            }
            return;
        }



        bullets.Clear();//������У�Ȼ���ٴ����
        //�������һ���ӵ�����������Ϊ��һ��
        int listLength = BulletManager.Instance.loadedBulletList.Count;
        int index = Random.Range(0, listLength);
        for (int i = index; i < listLength * 2; i++)
        {
            bullets.Add(BulletManager.Instance.loadedBulletList[i%listLength]);
            if (i % listLength == index - 1) break;//�ֵ�ͬ��Ԫ�ص�ǰһ����ͣ��
        }
        if(bullets.Count > BulletManager.Instance.loadedBulletList.Count)//��֤����ֻ��8������Ϊ��ʱ���16����bug
        {
            bullets.RemoveRange(BulletManager.Instance.loadedBulletList.Count, bullets.Count - BulletManager.Instance.loadedBulletList.Count);
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
        bulletIndexShoot = 0;
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

                #region ������
                case 10005:
                    OddBullet();
                    break;
                #endregion

                #region ż����
                case 10006:
                    EvenBullet();
                    break;
                #endregion

                #region ������
                case 10007:
                    PrimeBullet();
                    break;
                #endregion

                default:
                    bulletIndexBeforeShoot++;
                    break;
            }
        }
        if_ShootStart = true;
        StartCoroutine(StartShootAfterSpin());
    }
    public void StartShoot()//��ʼ�������Һ͵��˵Ķ�������
    {
        if (ShootEnd()) return;
        JudgeWhoShootImage(if_PlayerShoot, !if_PlayerShoot);//�غ�ͼƬ��ʾ
        if (if_PlayerShoot)
        {
            playerShootButton.gameObject.SetActive(true);
            //�����ť�󲥷ſ�ǹ����
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(currentEnemy.EnemyShooting());
        }
    }

    [Header("�ж�����˭�ȿ�ǹ���������ã�")]
    public bool if_PlayerShoot;
    public void SetIfPlayerFirst(bool setting)//������ť����Ҿ����Ⱥ�ǹ˳��
    {
        if_PlayerShoot = setting;
    }
    public bool JudegeShoot()//��ʼ������ж��ӵ�����,�����Ƿ��ǹ���ж�
    {
        if (bulletIndexShoot >= bullets.Count) StartShoot();
        //�ж��ӵ�

        #region �հ���
        if (bullets[bulletIndexShoot].ID == 10000 && bullets[bulletIndexShoot].actualDamage == 0)
        {
            Shoot();
            bulletIndexShoot++;
            return false;
        }
        #endregion

        #region ���ӵ��ж�
        if (!if_haveJudgedFirstShootFailed && bullets[bulletIndexShoot].ID==10003)
        {
            if_PlayerShoot = !if_PlayerShoot;
            if_haveJudgedFirstShootFailed = true;
            AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_MissFire);
            return false;
        }
        else
            if_haveJudgedFirstShootFailed = false;
        #endregion

        #region �������ж�
        if (ShootTwice())
        {
            return true;
        }
        #endregion

        #region �����ӵ���ֱ�����������
        Shoot();
        bulletIndexShoot++;
        return true;
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
            Debug.Log($"������1��{bullets[BulletIndexShoot]}");
            return;
        }
        else if (!if_PlayerShoot)
        {
            currentEnemy.EnemyGetHurt(damage);
            if_PlayerShoot = true;
            Debug.Log($"�������1��{bullets[BulletIndexShoot]}");
            return;
        }
    }
    public bool ShootEnd()//�������
    {
        if (InventoryManager.Instance.playerCurrentHealth <= 0)
        {
            endPage.gameObject.SetActive(true);
            endPage.Lose();
            return true;
        }
        else if (currentEnemy.CurrentHealth <= 0)
        {
            endPage.gameObject.SetActive(true);
            endPage.Win();
            return true;
        }
        if (bulletIndexShoot>bullets.Count-1)
        {
            if_haveJudgedFirstShootFailed = false;//��һ�������ǹ�жϻָ�
            bulletIndexShoot = 0;
            //�����Ĵ���
            endPage.gameObject.SetActive(true);
            endPage.Lose();
            return true;
        }
        
        return false;
    }
    IEnumerator StartShootAfterSpin()//�������������ʼ���ж�
    {
        battlePage.GetComponent<BattlePage>().SetRevolverBulletRotate(true);//������ת����
        while(AudioManager.Instance.AudioSource2EffectSource.isPlaying)
        {
            yield return null;
        }
        battlePage.GetComponent<BattlePage>().SetRevolverBulletRotate(false);
        StartShoot();
    }
    public void JudgeWhoShootImage(bool player,bool enemy)//˭�����ͼƬ����
    {
        playerInfoPage.huiHe.SetActive(player);
        enemyInfoPage.huiHe.SetActive(enemy);
    }

    #region ��Ӱ����е��ӵ�����
    public void NotTheFirst()//��ֵ�
    {
        RandomBulletQueue();//�Ǿ���������
    }
    public void OddBullet()//������
    {
        if (oddList.Contains(bullets[0]))
        {
            RandomBulletQueue();
            JudgeListEnable();
        }
        else bulletIndexBeforeShoot++;
    }
    public void EvenBullet()//ż����
    {
        if (evenList.Contains(bullets[0]))
        {
            RandomBulletQueue();
            JudgeListEnable();
        }
        else bulletIndexBeforeShoot++;
    }
    public void PrimeBullet()//������
    {
        if (primeList.Contains(bullets[0]))
        {
            RandomBulletQueue();
            JudgeListEnable();
        }
        else bulletIndexBeforeShoot++;
    }

    //���������ӵ��Ķ����ڴˣ�
    public List<Bullet> oddList = new List<Bullet>();
    public List<Bullet> evenList = new List<Bullet>();
    public List<Bullet> primeList = new List<Bullet>();
    //�����б��ʼ��
    void InitializeOddList()
    {
        oddList.Clear();
        for(int i=0;i<BulletManager.Instance.loadedBulletList.Count;i+=2)//�����е������ӵ�װ����
        {
            oddList.Add(BulletManager.Instance.loadedBulletList[i]);
        }
    }
    void InitializeEvenList()
    {
        evenList.Clear();
        for (int i = 1; i < BulletManager.Instance.loadedBulletList.Count; i += 2)//�����е�ż���ӵ�װ����
        {
            evenList.Add(BulletManager.Instance.loadedBulletList[i]);
        }
    }
    void InitializePrimeList()
    {
        primeList.Clear();
        for (int i = 1; i < BulletManager.Instance.loadedBulletList.Count; i += 1)//�����е������ӵ�װ����
        {
            if (i == 1 || i == 2 || i == 4 || i == 6)
                primeList.Add(BulletManager.Instance.loadedBulletList[i]);
        }
    }
    #endregion

    #region ��Ӱ����е��ӵ�����
    public bool if_haveJudgedFirstShootFailed;//�ж��Ƿ��Ѿ��ж��˵�һ�����ʧЧ
    /*public bool TheFirstShootFailed()//���ӵ�
    {
        if(bullets.Find(x=>x.ID==10003))
        {
            if_haveJudgedFirstShootFailed = true;
            return true;
        }
        return false;
    }*/
    public bool ShootTwice()//������
    {
        if (bullets[bulletIndexShoot].ID==10004)
        {
            bullets[bulletIndexShoot].actualDamage = 0;
            Shoot();
            bulletIndexShoot++;
            if_PlayerShoot = !if_PlayerShoot;//����ͬһ������
            JudegeShoot();
            return true;
        }
        return false;
    }
    #endregion
}
