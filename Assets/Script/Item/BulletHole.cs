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
    public bool if_CanBeLoad;//�ж����������Ƿ�������װ�Լ����ӵ�
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = unLoadSprite;//��ʼ����Ϊδװ��״̬
        if_CanBeLoad = false;//��ʼ��ҿ���װ��
    }

    void Update()
    {
        
    }

    #region װ���ӵ�
    public Sprite unLoadSprite;
    public Sprite loadSprite;//װ���ӵ�

    public void  LoadBulletPlayer()//���װ���ӵ�
    {
        if (!if_CanBeLoad) return;
        if (!if_Load && BulletManager.Instance.currentBullet != null)
        {
            if_Load = true;
            currentBullet = BulletManager.Instance.currentBullet;//��¼����װ����ӵ�

            image.sprite = currentBullet.sprite;//���ó�װ��ͼƬ��ʽ

            BulletManager.Instance.currentBullet.gameObject.SetActive(false);//���б��е��ӵ�����

            BulletManager.Instance.currentBullet = null;
        }
        else if (currentBullet != null)
        {
            if_Load = false;

            currentBullet.gameObject.SetActive(true);//����ѡ���ӵ�

            image.sprite = unLoadSprite;//���ó�δװ��ͼƬ��ʽ

            currentBullet = null;
        }

    }

    public void LoadBulletAuto(BulletType bulletType)//���Գ�ʼװ�ӵ�
    {
        if_CanBeLoad = true;
        currentBullet = BulletManager.Instance.bulletDictionary[bulletType].GetComponent<Bullet>();
        image.sprite = currentBullet.sprite;//���ó�װ��ͼƬ��ʽ
    }
    #endregion
}
