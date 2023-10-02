using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    public int currentLevelId;
    [System.Serializable]
    public struct LevelInfo
    {
        public int levelID;
        public string targetScene;
        public int enemyID;
        [Header("�̶�����id��λ����Ҫ��Ӧ")]
        public int[] steadyBulletID;//�̶��ӵ�����id
        public int[] steadyBulletPosition;//�̶��ӵ�λ��
        public int[] ableBulletID;//��Ҳ������ӵ�
    }
    [SerializeField]
    public List<LevelInfo> levelList = new List<LevelInfo>();//�ؿ��б�
    public Dictionary<int, LevelInfo> levelDictionary = new Dictionary<int, LevelInfo>();//�ؿ��ֵ����ڼ���

    private void Awake()
    {
        if(instance == null)instance = this;
        else Destroy(instance);
        DontDestroyOnLoad(this);

        InitializeLevelDictionary();
    }
    public void InitializeLevelDictionary()//��ʼ���ֵ�
    {
        for(int i = 0; i < levelList.Count; i++)
        {
            levelDictionary.Add(levelList[i].levelID, levelList[i]);
        }
    }
    public void SetCurrentLevel(int levelId)//����ťʹ�õ����õ�ǰ�ؿ�
    {
        currentLevelId = levelId;
    }
    public List<int> EnableBulletIdJudge(List<int> list)//�ṩ����ҵĿ����ӵ�
    {
        int[] steadyBulletId = levelDictionary[currentLevelId].ableBulletID;
        for(int i=0;i<steadyBulletId.Length;i++)
        {
            if(list.Contains(steadyBulletId[i]))
                list.Remove(steadyBulletId[i]);
        }
        return list;
    }
    public int[] GetCurentLevelSteadyBulletId()//�ṩ��ǰ�ؿ����õĹ̶��ӵ�id
    {
        return levelDictionary[currentLevelId].steadyBulletID;
    }
    public int[] GetCurentLevelSteadyBulletPosition()//�ṩ��ǰ�ؿ����õĹ̶��ӵ�λ�õ�id
    {
        return levelDictionary[currentLevelId].steadyBulletPosition;
    }
}
