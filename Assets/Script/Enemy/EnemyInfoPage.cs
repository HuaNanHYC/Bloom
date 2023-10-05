using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyInfoPage : MonoBehaviour
{
    private string enemyName;
    private float enemyHealth;//�������
    private float currentHealth;//��������
    private Sprite headImage;//ͷ��

    public Text enemyNameText;
    //public Slider enemyHealthSlide;
    public Image enemyHeadImage;
    public Text enemyHealthText;
    public Transform healthManage;//Ѫ��ͼƬ���ɹ���ĸ�����
    public GameObject healthImagePrefab;//Ѫ��ͼƬ

    private Enemy enemy;

    private List<Enemy.KeyWordAndDesc> keyWordAndDescsList = new List<Enemy.KeyWordAndDesc>();
    public GameObject keywordPrefab;//������ʾ�ؼ��ʵ�Ԥ����
    private void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        enemy.InitializeEnemyImageAndIcon();
        InitializePage();
    }
    private void Update()
    {
        UpdateEnemyInfoPage();
    }
    public void InitializePage()//�������ݳ�ʼ��
    {
        enemyName = enemy.enemyName;
        headImage = enemy.HeadImage;
        enemyHealth = enemy.enemyHealth;

        enemyNameText.text = enemyName;
        if (enemy.HeadImage != null)
            enemyHeadImage.sprite = enemy.HeadImage;
        keyWordAndDescsList = enemy.keyWordAndDescsList;

        //enemyHealthSlide.maxValue = enemyHealth;

        ShowTheKeyWords();
        InstantiateBloodImage();//����Ѫ��ͼƬ
    }
    public void UpdateEnemyInfoPage()//ʵʱ���µ���Ѫ��������
    {
        currentHealth = enemy.CurrentHealth;
        //enemyHealthSlide.value = currentHealth;
        enemyHealthText.text = currentHealth.ToString() + "/" + enemyHealth.ToString();

        float falseBlood = enemyHealth - currentHealth;
        for(int i = healthManage.childCount-1; i > currentHealth-1; i--)
        {
            if(healthManage.GetChild(i) != null)
            {
                if (healthManage.GetChild(i).GetChild(0).gameObject.activeSelf)
                    healthManage.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void ShowTheKeyWords()
    {
        for (int i = 0; i < keyWordAndDescsList.Count; i++)
        {
            keywordPrefab.GetComponentInChildren<Text>().text = keyWordAndDescsList[i].keyWord;
            keywordPrefab.GetComponent<EnemyKeywordShow>().Description = keyWordAndDescsList[i].keyDecription;
            keywordPrefab.GetComponent<EnemyKeywordShow>().keywordDescriptionShow.transform.GetChild(0).GetComponent<Text>().text = keyWordAndDescsList[0].keyWord;
        }
    }
    public void InstantiateBloodImage()
    {
        for(int i=0;i<enemyHealth;i++)
        {
            Instantiate(healthImagePrefab, healthManage);
        }
    }
}
