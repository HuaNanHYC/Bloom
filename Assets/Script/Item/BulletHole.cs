using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletHole : MonoBehaviour
{
    private Image image;
    public int number;//���ֶ������
    public Bullet currentBullet;
    public bool if_Load;
    public bool if_AutoLoad;//�ж����������Ƿ�������װ�Լ����ӵ�
    private void Awake()
    {
        if_AutoLoad = false;//��ʼ��ҿ���װ��
        image = GetComponent<Image>();
    }
    void Start()
    {
        if(image.sprite==null)
            image.sprite = unLoadSprite;//��ʼ����Ϊδװ��״̬
    }

    void Update()
    {
        
    }

    #region װ���ӵ�
    public Sprite loadedSprite;//�Ѿ�װ�����ʽ
    public Sprite unLoadSprite;
    public Sprite loadSprite;//װ���ӵ�

    public void  LoadBulletPlayer()//���װ���ӵ�
    {
        if (if_AutoLoad) return;
        if (!if_Load && BulletManager.Instance.currentBullet != null)
        {
            if (BulletManager.Instance.JudgeExistTogether_IfCanPutIn(BulletManager.Instance.currentBullet.ID) == false) return;
            if_Load = true;
            currentBullet = BulletManager.Instance.currentBullet;//��¼����װ����ӵ�

            image.sprite = loadedSprite;//���ó�װ��ͼƬ��ʽ
            UpdateLoadedSpriteInChild(true);

            //BulletManager.Instance.currentBullet.gameObject.SetActive(false);//���б��е��ӵ�����

            BulletManager.Instance.currentBullet = null;
        }
        else if (currentBullet != null)
        {
            if_Load = false;

            //currentBullet.gameObject.SetActive(true);//����ѡ���ӵ�

            image.sprite = unLoadSprite;//���ó�δװ��ͼƬ��ʽ
            UpdateLoadedSpriteInChild(false);

            currentBullet = null;
        }

    }

    public void LoadBulletAuto(int bulletID)//���Գ�ʼװ�ӵ�
    {
        if_AutoLoad = true;
        currentBullet = BulletManager.Instance.bulletDictionary[bulletID].GetComponent<Bullet>();
        currentBullet.BulletIcon = Resources.Load<Sprite>(currentBullet.bulletIconPath);
        if (currentBullet.BulletIcon == null) return;
        image.sprite = loadedSprite;//���ó�װ��ͼƬ��ʽ
        UpdateLoadedSpriteInChild(true);
    }

    public void UpdateLoadedSpriteInChild(bool if_setIcon)//����һ�����������������ӵ���icon
    {
        if (if_setIcon)
        {
            if (!transform.Find("bulletIcon"))
            {
                GameObject childIcon = new GameObject("bulletIcon");
                childIcon.transform.SetParent(transform,false);
                Image childIconImage = childIcon.AddComponent<Image>();
                childIconImage.sprite = currentBullet.BulletIcon;
            }
            else
            {
                Image childIconImage = transform.Find("bulletIcon").GetComponent<Image>();
                childIconImage.sprite = currentBullet.BulletIcon;
            }
        }
        else
        {
            if (!transform.Find("bulletIcon"))
            {
                return;
            }
            else
            {
                Image childIconImage = transform.Find("bulletIcon").GetComponent<Image>();
                childIconImage.sprite = unLoadSprite;
            }
        }
    }
    #endregion
}
