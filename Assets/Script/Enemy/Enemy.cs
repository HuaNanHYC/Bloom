using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region ������Ϣ

    public int enemyID;
    public string enemyName;
    public float enemyHealth;
    [TextArea]
    public string headImagePath;//ͼƬ·��
    private Sprite headImage;//ͷ��
    [Header("ͼƬ·��")]
    [TextArea]
    public string dialogueImagePath;//ͼƬ·��
    private Sprite dialogueImage;//�Ի�����
    [Header("���кͿ�ǹ�����йص�ͼƬ·��")]
    [TextArea] public string actionImagePath;
    [TextArea] public string readyImagePath;   
    [TextArea] public string dodgeImagePath;
    [TextArea] public string shotImagePath;
    private Sprite actionImage;//��ǹ��������
    private Sprite readyImage;//׼����ǹ����
    private Sprite dodgeImage;//δ��ǹ���� 
    private Sprite shotImage;//��ǹ����

    private SpriteRenderer enemySpriteRenderer;//����ͼ�����
    private Animator animator;//������
    private BattleSystem battleSystem;//ս��ϵͳ
    [System.Serializable]
    public struct KeyWordAndDesc
    {
        public string keyWord;
        [TextArea]
        public string keyDecription;
    }
    [SerializeField]
    public List<KeyWordAndDesc> keyWordAndDescsList = new List<KeyWordAndDesc>();

    public Sprite HeadImage { get => headImage; }
    public Sprite DialogueImage { get => dialogueImage; }
    public float CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public BattleSystem BattleSystem { get => battleSystem; set => battleSystem = value; }

    #endregion
    protected void Awake()
    {
        enemySpriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        InitializeEnemy();
    }
    protected void Start()
    {
        InitializeEnemyImageAndIcon();
    }

    protected void Update()
    {

    }

    protected void InitializeEnemyImageAndIcon()//��ʼ�����ص��˵�����ͼƬ
    {
        headImage = Resources.Load<Sprite>(headImagePath);
        dialogueImage = Resources.Load<Sprite>(dialogueImagePath);

        actionImage= Resources.Load<Sprite>(actionImagePath);
        readyImage = Resources.Load<Sprite>(readyImagePath);
        dodgeImage = Resources.Load<Sprite>(dodgeImagePath);
        shotImage = Resources.Load<Sprite>(shotImagePath);
    }
    public void InitializeEnemy()
    {
        currentHealth = enemyHealth;
    }

    #region ��ս�����
    private float currentHealth;//���ڵ�Ѫ��
    public void EnemyGetHurt(float damage)//�����ܵ��˺�
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }
    public IEnumerator EnemyShooting()//���˿�ǹ
    {
        //������ǹ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        enemySpriteRenderer.sprite = actionImage;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = readyImage;
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        if (battleSystem.JudegeShoot())
        {
            enemySpriteRenderer.sprite = shotImage;//��ǹ
            //�ӵ���ը��ͼƬ
        }
        else enemySpriteRenderer.sprite = dodgeImage;//û��ǹ
        //�ص���ʼװ̬�����˰�ǹ�Ż�
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = readyImage;
        yield return new WaitForSeconds(0.5f);
        enemySpriteRenderer.sprite = actionImage;
        battleSystem.StartShoot();
    }

    #endregion
}
