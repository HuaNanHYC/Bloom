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

            image.sprite = currentBullet.BulletIcon;//���ó�װ��ͼƬ��ʽ

            //BulletManager.Instance.currentBullet.gameObject.SetActive(false);//���б��е��ӵ�����

            BulletManager.Instance.currentBullet = null;
        }
        else if (currentBullet != null)
        {
            if_Load = false;

            //currentBullet.gameObject.SetActive(true);//����ѡ���ӵ�

            image.sprite = unLoadSprite;//���ó�δװ��ͼƬ��ʽ

            currentBullet = null;
        }

    }

    public void LoadBulletAuto(int bulletID)//���Գ�ʼװ�ӵ�
    {
        if_AutoLoad = true;
        currentBullet = BulletManager.Instance.bulletDictionary[bulletID].GetComponent<Bullet>();
        currentBullet.BulletIcon = Resources.Load<Sprite>(currentBullet.bulletIconPath);
        if (currentBullet.BulletIcon == null) return;
        image.sprite = currentBullet.BulletIcon;//���ó�װ��ͼƬ��ʽ
    }
    #endregion
}
