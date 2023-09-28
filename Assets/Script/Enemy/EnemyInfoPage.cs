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
    public Slider enemyHealthSlide;
    public Image enemyHeadImage;
    public Text enemyHealthText;

    private Enemy enemy;

    private List<Enemy.KeyWordAndDesc> keyWordAndDescsList = new List<Enemy.KeyWordAndDesc>();
    public Transform infoParent;//��Ϣ��
    public GameObject keywordPrefab;//������ʾ�ؼ��ʵ�Ԥ����
    private void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
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

        enemyHealthSlide.maxValue = enemyHealth;

        ShowTheKeyWords();
    }
    public void UpdateEnemyInfoPage()//ʵʱ���µ���Ѫ��������
    {
        currentHealth = enemy.CurrentHealth;
        enemyHealthSlide.value = currentHealth;
        enemyHealthText.text = currentHealth.ToString() + "/" + enemyHealth.ToString();
    }
    public void ShowTheKeyWords()
    {
        for (int i = 0; i < keyWordAndDescsList.Count; i++)
        {
            GameObject keyWordInfo = Instantiate(keywordPrefab, infoParent);
            keyWordInfo.GetComponentInChildren<Text>().text = keyWordAndDescsList[i].keyWord;
            keyWordInfo.GetComponent<EnemyKeywordShow>().Description = keyWordAndDescsList[i].keyDecription;
        }
    }
}
