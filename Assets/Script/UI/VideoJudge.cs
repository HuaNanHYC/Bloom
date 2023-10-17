using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
public class VideoJudge : MonoBehaviour
{
    public VideoPlayer startPlayer;
    public VideoPlayer endPlayer;
    private void Awake()
    {
        // ���ÿ�ʼ��Ƶ
        string startVideoPath = Path.Combine(Application.streamingAssetsPath, "S_01_mp3.mp4");
        startPlayer.url = startVideoPath;
        startPlayer.Prepare();

        // ���ý�����Ƶ
        string endVideoPath = Path.Combine(Application.streamingAssetsPath, "S_E_mp3.mp4");
        endPlayer.url = endVideoPath;
        endPlayer.Prepare();
    }
    void Start()
    {

    }
    private void OnEnable()
    {
        if(!LevelManager.Instance.StartVideoPlay)
        {
            startPlayer.Play();
            startPlayer.loopPointReached += OnStartVideoEnd;
            LevelManager.Instance.StartVideoPlay = true;
            return;
        }
        if(!LevelManager.Instance.EndVideoPlay)
        {
            endPlayer.Play();
            endPlayer.loopPointReached += OnEndVideoEnd;
            LevelManager.Instance.StartVideoPlay = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void OnStartVideoEnd(VideoPlayer vp)
    {
        if (vp == startPlayer)
        {
            LevelManager.Instance.NextLevel();
            Debug.Log("��ͷ��Ƶ������ɣ�");
        }
    }
    public void OnEndVideoEnd(VideoPlayer vp)
    {
        if (vp == endPlayer)
        {
            UIManager.Instance.LoadScene("Menu");
            Debug.Log("��β��Ƶ������ɣ�");
        }
    }
}
