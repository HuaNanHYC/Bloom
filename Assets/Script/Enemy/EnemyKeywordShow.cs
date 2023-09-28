using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnemyKeywordShow : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler
{
    public GameObject keywordDescriptionShow;
    private RectTransform keywordDescriptionRectTransform;//��ͣ��ʾUI��λ��
    private string description;
    public string Description { get => description; set => description = value; }
    private void Start()
    {
        keywordDescriptionRectTransform = keywordDescriptionShow.GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        keywordDescriptionShow.SetActive(true);
        keywordDescriptionShow.GetComponentInChildren<Text>().text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        keywordDescriptionShow?.SetActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2 localMousePos;
        // ��ȡUIԪ�صĿ��
        float uiElementWidth = keywordDescriptionRectTransform.rect.width;
        float uiElementHeight = keywordDescriptionRectTransform.rect.height;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                keywordDescriptionRectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos))
        {
            // ����UIԪ�صĴ�С�Զ�����ƫ������ȷ�����ص�
            Vector2 offset = new Vector2(uiElementWidth / 1.9f, -uiElementHeight );

            // ����UIԪ�ص�λ��
            keywordDescriptionRectTransform.localPosition = localMousePos + offset;
        }
    }
}
