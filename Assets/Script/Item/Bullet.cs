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
    /*[TextArea]
    public string extraDescription;*/
    public float settingDamage=1;//�趨���˺�
    public float actualDamage;//ʵ�ʵ��˺�

    private Sprite bulletIcon;
    private Sprite bulletImageMain;
    private Sprite bulletImageHover;
    private Sprite bulletImageSelected;
    [TextArea] public string bulletIconPath;
    [TextArea] public string bulletImagePath;
    [TextArea] public string bulletImageHoverPath;
    [TextArea] public string bulletImageSelectedPath;

    private Image bulletImageSelf;
    private bool if_OnlyUseBullet;
    private void Start()
    {
        if (if_OnlyUseBullet) return;
        bulletInfoRectTransform = currentBulletInfo.GetComponent<RectTransform>();
        bulletImageSelf = transform.GetComponent<Image>();
        UpdateBulletImageAndIcon();//����ͼƬ
        InitializeBullet();
        UpdateBulletInfo();//ͬ����Ϣ
    }
    private void Update()
    {
        if (if_OnlyUseBullet) return;
        IfSelected();
    }


    #region �����ͣ��ʾ����ui
    private RectTransform bulletInfoRectTransform;//UI��λ��
    public GameObject currentBulletInfo;

    public Sprite BulletIcon { get => bulletIcon; set => bulletIcon = value; }//ֻ�ɶ�
    //public Sprite BulletImage { get => bulletImage;}
    public int BulletInHoleNumber { get => bulletInHoleNumber; set => bulletInHoleNumber = value; }
    public bool If_OnlyUseBullet { get => if_OnlyUseBullet; set => if_OnlyUseBullet = value; }

    public void UpdateBulletInfo()//����Ϣ����
    {
        currentBulletInfo.transform.GetChild(0).GetComponent<Text>().text = bulletName;
        currentBulletInfo.transform.GetChild(1).GetComponent<Text>().text = bulletDescription;
        //currentBulletInfo.transform.GetChild(2).GetComponent<Text>().text = extraDescription;
    }
    private bool if_PointerEnter;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if_PointerEnter = true;

        // ���������ʱ��ʾ��ʾ
        currentBulletInfo.SetActive(true);
        //ͼƬ����任
        bulletImageSelf.sprite = bulletImageHover;
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if_PointerEnter = false;

        // ����뿪����ʱ������ʾ
        currentBulletInfo.SetActive(false);

        bulletImageSelf.sprite = bulletImageMain;
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

   public void IfSelected()
    {
        if(BulletManager.Instance.currentBullet==this)
        {
            bulletImageSelf.sprite = bulletImageSelected;
            transform.GetChild(0).GetComponent<Text>().color=Color.white;
        }
        else if(if_PointerEnter==false)
        {
            bulletImageSelf.sprite = bulletImageMain;
            transform.GetChild(0).GetComponent<Text>().color = new Color(0.9568627f, 0.4392157f, 0.1607843f);
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
        bulletIcon = Resources.Load<Sprite>(bulletIconPath);
        bulletImageMain = Resources.Load<Sprite>(bulletImagePath);
        bulletImageHover = Resources.Load<Sprite>(bulletImageHoverPath);
        bulletImageSelected = Resources.Load<Sprite>(bulletImageSelectedPath);

        bulletImageSelf.sprite = bulletImageMain;
    }

    #endregion
}
