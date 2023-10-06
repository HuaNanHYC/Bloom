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
    [TextArea] public string shotImagePath;
    public SpriteRenderer shootText;//��ǹ���ǹ�����֣���������ĸ���
    public Sprite dodgeActionText;//��ǹ����
    public Sprite dodgeText;//��ǹ��
    public Sprite shotText;//��ǹ��
    protected Sprite actionImage;//��ǹ��������
    protected Sprite readyImage;//׼����ǹ����
    protected Sprite shotImage;//��ǹ����

    protected SpriteRenderer enemySpriteRenderer;//����ͼ�����
    protected Animator animator;//������
    protected BattleSystem battleSystem;//ս��ϵͳ
    protected GameObject gunSprite;
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
        gunSprite = GameObject.FindWithTag("Gun");
        InitializeEnemyImageAndIcon();
    }
    public void InitializeEnemyImageAndIcon()//��ʼ�����ص��˵�����ͼƬ
    {
        headImage = Resources.Load<Sprite>(headImagePath);
        dialogueImage = Resources.Load<Sprite>(dialogueImagePath);

        actionImage= Resources.Load<Sprite>(actionImagePath);
        readyImage = Resources.Load<Sprite>(readyImagePath);
        shotImage = Resources.Load<Sprite>(shotImagePath);
    }
    public void InitializeEnemy()
    {
        currentHealth = enemyHealth;
    }

    #region ��ս�����
    protected float currentHealth;//���ڵ�Ѫ��
    protected bool if_Immute=false;//�Ƿ�����
    public void EnemyGetHurt(float damage)//�����ܵ��˺�
    {
        if (if_Immute && damage != 0)
        {
            AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_MissFire);
            if_Immute = false;
            return;
        }
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        /*if(damage == 0 && battleSystem.bullets[battleSystem.BulletIndexShoot].ID==10003)//���ӵ��ж�
        {
            AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_MissFire);
            return;
        }*/
        if (damage > 0)
        {
            AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_Fire);//��������
        }
        else
        {
            AudioManager.Instance.AudioSource2EffectSource.PlayOneShot(AudioManager.Instance.Revolver_NoBullet);//�յ�����
        }
    }
    
    public virtual IEnumerator EnemyShooting()//���˿�ǹ
    {
        //������ǹ
        yield return new WaitForSeconds(0.5f);//�ȴ�0.5��
        EnemyAction(true);
        yield return new WaitForSeconds(0.5f);
        EnemyReady(false);
        
        //׼����ǹ
        yield return new WaitForSeconds(0.5f);
        if (battleSystem.JudegeShoot())
        {
            EnemyShot();//��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyAction(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }
        else
        {
            EnemyDodge();//û��ǹ
            yield return new WaitForSeconds(0.5f);
            EnemyReady(false);
            yield return new WaitForSeconds(0.5f);
            EnemyDodgeAction();
            actionHand.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            actionHand.SetActive(false);
            EnemyIdle();
            battleSystem.StartShoot();
            StopAllCoroutines();
        }

    }

    protected void EnemyDodgeAction()
    {
        enemySpriteRenderer.sprite = actionImage;
        shootText.sprite = dodgeActionText;
    }

    protected void EnemyDodge()
    {
        enemySpriteRenderer.sprite = readyImage;
        shootText.sprite = dodgeText;//���յ�����
    }

    protected void EnemyIdle()
    {
        gunSprite.SetActive(true);
        enemySpriteRenderer.sprite = dialogueImage;
        shootText.sprite = null;
    }

    protected void EnemyShot()
    {
        enemySpriteRenderer.sprite = shotImage;
        shootText.sprite = shotText;
    }

    protected void EnemyReady(bool hand)
    {
        enemySpriteRenderer.sprite = readyImage;
        actionHand.SetActive(hand);
        shootText.sprite = null;
    }

    protected void EnemyAction(bool hand)
    {
        gunSprite.SetActive(false);
        enemySpriteRenderer.sprite = actionImage;
        actionHand.SetActive(hand);
        shootText.sprite = null;
    }


    #endregion
}
