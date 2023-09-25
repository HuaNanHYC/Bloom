using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public enum BulletType
{
    NullBullet,
    NormalBullet,
}

public class Bullet : MonoBehaviour,IPointerEnterHandler,IPointerMoveHandler,IPointerExitHandler
{
    public int ID;
    public string bulletName;
    public string description;
    public BulletType type;
    public float damage;
    public Sprite sprite;

    private void Start()
    {
        bulletInfoRectTransform = currentBulletInfo.GetComponent<RectTransform>();
        UpdateBulletInfo();//ͬ����Ϣ
    }


    #region �����ͣ��ʾ����ui
    private RectTransform bulletInfoRectTransform;//UI��λ��
    public GameObject currentBulletInfo;
    public void UpdateBulletInfo()//����Ϣ����
    {
        currentBulletInfo.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        currentBulletInfo.transform.GetChild(0).GetComponentInChildren<Text>().text = bulletName;
        currentBulletInfo.transform.GetChild(1).GetComponent<Text>().text = description;
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

    #region �ӵ�Ч��ѡ�񣬸��������ж�
    protected virtual void Effect()
    {
        //�ӵ�Ч����д���̳е�����
    }


    #endregion
}
