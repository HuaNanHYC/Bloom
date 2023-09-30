using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour,IPointerEnterHandler,IPointerMoveHandler,IPointerExitHandler
{
    public int ID;
    public string bulletName;
    private int bulletInHoleNumber;
    [TextArea]
    public string bulletDescription;
    [TextArea]
    public string extraDescription;
    public float settingDamage=1;//�趨���˺�
    public float actualDamage;//ʵ�ʵ��˺�

    private Sprite bulletIcon;
    private Sprite bulletImage;
    [TextArea]
    public string bulletIconPath;
    [TextArea]
    public string bulletImagePath;

    private void Start()
    {
        bulletInfoRectTransform = currentBulletInfo.GetComponent<RectTransform>();
        bulletImage = GetComponent<Image>().sprite;
        UpdateBulletImageAndIcon();//����ͼƬ
        InitializeBullet();
        UpdateBulletInfo();//ͬ����Ϣ
    }


    #region �����ͣ��ʾ����ui
    private RectTransform bulletInfoRectTransform;//UI��λ��
    public GameObject currentBulletInfo;

    public Sprite BulletIcon { get => bulletIcon; }//ֻ�ɶ�
    public Sprite BulletImage { get => bulletImage;}
    public int BulletInHoleNumber { get => bulletInHoleNumber; set => bulletInHoleNumber = value; }

    public void UpdateBulletInfo()//����Ϣ����
    {
        currentBulletInfo.transform.GetChild(0).GetComponent<Image>().sprite = bulletIcon;
        currentBulletInfo.transform.GetChild(0).GetComponentInChildren<Text>().text = bulletName;
        currentBulletInfo.transform.GetChild(1).GetComponent<Text>().text = bulletDescription;
        currentBulletInfo.transform.GetChild(2).GetComponent<Text>().text = extraDescription;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���������ʱ��ʾ��ʾ
        currentBulletInfo.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // ����뿪����ʱ������ʾ
        currentBulletInfo.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2 localMousePos;
        // ��ȡUIԪ�صĿ��
        float uiElementWidth = bulletInfoRectTransform.rect.width;
        float uiElementHeight = bulletInfoRectTransform.rect.height;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                bulletInfoRectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos))
        {
            // ����UIԪ�صĴ�С�Զ�����ƫ������ȷ�����ص�
            Vector2 offset = new Vector2(-uiElementWidth / 1.9f, -uiElementHeight / 1.9f);

            // ����UIԪ�ص�λ��
            bulletInfoRectTransform.localPosition = localMousePos + offset;
        }
    }
    #endregion

    #region ���ڰ�ť����
    public void SetCurrentBullet()//��������ѡ����ӵ�
    {
        BulletManager.Instance.currentBullet = this;
    }

    #endregion

    #region �ӵ���ʼ��
    public void InitializeBullet()
    {
        actualDamage = settingDamage;//�ָ��˺�
    }

    public void UpdateBulletImageAndIcon()
    {
        Sprite imageSprite = Resources.Load<Sprite>(bulletIconPath);
        if (imageSprite != null)
        {
            // ��ȡ�����ϵ�Image���
            bulletIcon = imageSprite;
        }
        else
        {
            Debug.Log("û���ҵ�·��ͼƬ: " + bulletIconPath);
        }

        Sprite imageSprite2 = Resources.Load<Sprite>(bulletImagePath);
        if (imageSprite2 != null)
        {
            // ��ȡ�����ϵ�Image���
            bulletImage = imageSprite;
        }
        else
        {
            Debug.Log("û���ҵ�·��ͼƬ: " + bulletImagePath);
        }
    }
    #endregion
}
