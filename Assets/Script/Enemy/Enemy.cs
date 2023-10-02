using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region ������Ϣ

    public int enemyID;
    public string enemyName;
    public float enemyHealth;
    public GameObject actionHand;//��ǹ����
    [TextArea]
    public string headImagePath;//ͼƬ·��
    protected Sprite headImage;//ͷ��
    [Header("ͼƬ·��")]
    [TextArea]
    public string dialogueImagePath;//ͼƬ·��
    protected Sprite dialogueImage;//�Ի�����
    [Header("���кͿ�ǹ�����йص�ͼƬ·��")]
    [TextArea] public string actionImagePath;
    [TextArea] public string readyImagePath;   
    [TextArea] public string dodgeImagePath;
    [TextArea] public string shotImagePath;
    [TextArea] public string dodgeActionImagePath;
    protected Sprite actionImage;//��ǹ��������
    protected Sprite readyImage;//׼����ǹ����
    protected Sprite dodgeImage;//δ��ǹ���� 
    protected Sprite shotImage;//��ǹ����
    protected Sprite dodgeActionImage;//û��ǹ���ǹ������

    protected SpriteRenderer enemySpriteRenderer;//����ͼ�����
    protected Animator animator;//������
    protected BattleSystem battleSystem;//ս��ϵͳ
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
    protected virtual void Start()
    {
        InitializeEnemyImageAndIcon();
    }
    protected void InitializeEnemyImageAndIcon()//��ʼ�����ص��˵�����ͼƬ
    {
        headImage = Resources.Load<Sprite>(headImagePath);
        dialogueImage = Resources.Load<Sprite>(dialogueImagePath);

        actionImage= Resources.Load<Sprite>(actionImagePath);
        readyImage = Resources.Load<Sprite>(readyImagePath);
        dodgeImage = Resources.Load<Sprite>(dodgeImagePath);
        shotImage = Resources.Load<Sprite>(shotImagePath);
        dodgeActionImage = Resources.Load<Sprite>(dodgeActionImagePath);
    }
    public void InitializeEnemy()
    {
        currentHealth = enemyHealth;
    }

    #region ��ս�����
    protected float currentHealth;//���ڵ�Ѫ��
    protected bool if_Immute=false;//�Ƿ�����
    public void EnemyGetHurt(float damage,int bulletIndex)//�����ܵ��˺�
    {
        if (if_Immute && bulletIndex == 0)
        {
            if_Immute = false;
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
    }
    
    public virtual IEnumerator EnemyShooting()//���˿�ǹ
    {
        //������ǹ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        enemySpriteRenderer.sprite = actionImage;
        actionHand.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        actionHand.SetActive(false);
        enemySpriteRenderer.sprite = readyImage;
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        if (battleSystem.JudegeShoot())
        {
            enemySpriteRenderer.sprite = shotImage;//��ǹ
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = readyImage;
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = actionImage;
            actionHand.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            enemySpriteRenderer.sprite = dialogueImage;
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        else
        {
            enemySpriteRenderer.sprite = dodgeImage;//û��ǹ
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = readyImage;
            yield return new WaitForSeconds(0.5f);
            enemySpriteRenderer.sprite = dodgeActionImage;
            actionHand.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            enemySpriteRenderer.sprite = dialogueImage;
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        
    }

    #endregion
}
