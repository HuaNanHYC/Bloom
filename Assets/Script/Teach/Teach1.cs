using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teach1 : MonoBehaviour
{
    public Text teachText;
    public Canvas[] canvas;
    public Canvas mainCanvas;
    public int clickIndex;
    public int maxIndex;
    public bool if_OpenLoadPage;
    private bool setOnce;
    public Vector3[] textPosition;
    private void Start()
    {
        if (UIManager.Instance.if_Teach1)
        {
            mainCanvas.gameObject.SetActive(false);
            return;
        }
            maxIndex = canvas.Length;
        NextTeach();
    }
    private void Update()
    {
        if (UIManager.Instance.if_Teach1)
        {
            return;
        }
        PlayerOperation();
    }
    
    public void NextTeach()
    {
        if (clickIndex < maxIndex)
            canvas[clickIndex].sortingOrder = 82;
        switch (clickIndex)
        {
            case 0:
                teachText.transform.parent.transform.localPosition = textPosition[0];
                teachText.text = "���ҵ�Ѫ������ʾ�����ȷ���Լ��������У�����һ�ֹ����ܽ��Է���Ѫ����0�ɣ�";
                break;
            case 1:
                teachText.transform.parent.transform.localPosition = textPosition[1];
                teachText.text = "�Է�����ǹ�Ѿ��ó����ˣ����װ����ʼװ���ӵ��ɣ�";
                break;
            case 2:
                teachText.transform.parent.transform.localPosition = textPosition[2];
                teachText.text = "������ʾ��������ǹ�ĵ����������ϵ�ÿ�����ֶ��ж�Ӧ�ı�ţ��ӵ���������ڵĵ��ֱ�Ų����ض���Ч��";
                break;
            case 3:
                teachText.transform.parent.transform.localPosition = textPosition[3];
                teachText.text = "������ʾ������ĵ�ҩ�����г�������ʹ�õ������ӵ�����ͣ���Բ鿴�ӵ��ľ���Ч��������ӵ��ٵ�����ⵯ�־Ϳ��Խ���װ������";
                break;
            default:break;
        }
    }
    private bool ifDelay;
    public void PlayerOperation()//�ж���ҵĵ������
    {
        if (clickIndex > maxIndex - 1)
        {
            UIManager.Instance.if_Teach1 = true;
            gameObject.SetActive(false);
            SetAllCanvas();
            return;
        }
        if (if_OpenLoadPage && !setOnce)
        {
            mainCanvas.gameObject.SetActive(true);
            clickIndex++;
            NextTeach();
            setOnce = true;
        }

        if (Input.GetMouseButtonDown(0)&&!ifDelay)
        {
            SetAllCanvas();
            if (clickIndex < 1 || if_OpenLoadPage)
            {
                if (ifDelay) return;
                clickIndex++;
                if(clickIndex==2)
                {
                    ifDelay = true;
                    StartCoroutine(NextTeachDelay());
                    return;
                }
                NextTeach();
            }
            else if(!if_OpenLoadPage)
            {
                mainCanvas.gameObject.SetActive(false);
                canvas[1].gameObject.SetActive(false);
            }
            
        }
    }
    public void SetAllCanvas()
    {
        canvas[0].sortingOrder = 0;
        canvas[1].sortingOrder = 0;
        canvas[2].sortingOrder = 2;
        canvas[3].sortingOrder = 3;
        teachText.text = "";
    }
    public void SetIfOpenLoadPage(bool setting)
    {
        if_OpenLoadPage=setting; 
    }//����ť�ж�

    IEnumerator NextTeachDelay()
    {
        yield return new WaitForSeconds(0.6f);
        NextTeach();
        ifDelay = false;
    }
}
