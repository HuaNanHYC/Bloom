using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManageSystem : MonoBehaviour
{
    private static SceneManageSystem instance;
    public static SceneManageSystem Instance { get { return instance; } }
    
    public void Awake()
    {
        if(instance == null)instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private bool ifAllowSceneActivation;//�ɹ�����������Ƿ���Լ���Ŀ�곡��

    public GameObject fadePrefab;//�������ɼ��ص�Ԥ����

    public bool IfAllowSceneActivation { get => ifAllowSceneActivation; set => ifAllowSceneActivation = value; }
    public void GoToFigureScene(string name)
    {
        StartCoroutine(ChangeScene(name));
    }

    AsyncOperation asyncOperation;
    static WaitForEndOfFrame _endOfFrame = new WaitForEndOfFrame();//����while
    IEnumerator ChangeScene(string name)
    {
        GameObject fade=Instantiate(fadePrefab);
        asyncOperation=SceneManager.LoadSceneAsync(name);
        asyncOperation.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(1);
        while(asyncOperation.progress<0.9f)
        {
            yield return _endOfFrame;
        }
        fade.GetComponent<FadePrefab>().IfDone = true;//fade�Ǳ߿�ʼ�ж�
        while (true)
        {
            if (IfAllowSceneActivation)
            {
                asyncOperation.allowSceneActivation = IfAllowSceneActivation;
                IfAllowSceneActivation = false;
                break;
            }
            yield return _endOfFrame;
        }
    }



}
