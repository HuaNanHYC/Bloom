using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BulletHole : MonoBehaviour,IPointerEnterHandler,IPointerMoveHandler,IPointerExitHandler
{
    private Image image;
    public int number;//���ֶ������
    public Bullet currentBullet;
    public bool if_Load;
    public bool if_AutoLoad;//�ж����������Ƿ�������װ�Լ����ӵ�
    public GameObject bulletDesc;//�ӵ�����������ʾ
    private GameObject descriptionBoard;
    private RectTransform bulletInfoRectTransform;
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
        if (currentBullet == null && descriptionBoard != null) descriptionBoard.SetActive(false);
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
    public void UpdateBulletInfo()//����Ϣ����
    {
        descriptionBoard.transform.GetChild(0).GetComponent<Text>().text = currentBullet.bulletName;
        descriptionBoard.transform.GetChild(1).GetComponent<Text>().text = currentBullet.bulletDescription;
        descriptionBoard.transform.GetComponent<Image>().SetNativeSize();
        //currentBulletInfo.transform.GetChild(2).GetComponent<Text>().text = extraDescription;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(currentBullet!=null)
        {
            if (descriptionBoard != null)
            {
                UpdateBulletInfo();
                descriptionBoard.SetActive(true);
                return;
            }
            descriptionBoard = Instantiate(bulletDesc);
            descriptionBoard.transform.SetParent(transform,false);
            bulletInfoRectTransform = descriptionBoard.GetComponent<RectTransform>();
            descriptionBoard.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
            UpdateBulletInfo();
        }
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (bulletInfoRectTransform == null) return;
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
            Vector2 offset = new Vector2(0, -uiElementHeight / 1f);

            // ����UIԪ�ص�λ��
            bulletInfoRectTransform.localPosition = localMousePos + offset;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(descriptionBoard!=null)
        {
            descriptionBoard.SetActive(false);
        }
    }
    #endregion
}
