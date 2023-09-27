using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePrefab : MonoBehaviour
{
    private bool ifDone;//�Ƿ���س������
    private bool ifPressAnyButton;//�Ƿ��������
    public GameObject pressAnyButtonToStart;//���������ʼ��Ϸ��ʾ�����
    private Animator animator;//�������
    public bool IfDone { get => ifDone; set => ifDone = value; }
    public bool IfPressAnyButton { get => ifPressAnyButton; set => ifPressAnyButton = value; }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(IfDone==true)
        {
            //pressAnyButtonToStart.SetActive(true);
            //if(Input.anyKey)
            //{
                //pressAnyButtonToStart.SetActive(false);
                IfPressAnyButton = true;
                IfDone = false;
                SceneManageSystem.Instance.IfAllowSceneActivation = true;
                animator.SetBool("FadeOut", true);
            //}
        }

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
