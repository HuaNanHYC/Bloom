using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct LevelInfo
    {
        public int iD;
        public GameObject prefab;
        public int levelID;
        public int enemyID;
        public int[] steadyBulletID;//�̶��ӵ�����id
        public int[] steadyBulletPosition;//�̶��ӵ�λ��
        public int[] enableBulletID;//��ҿ����ӵ�
        public int levelStartStoryID;
        public int levelOverStoryID;
    }
    [SerializeField]
    public List<LevelInfo> levelList = new List<LevelInfo>();//�ؿ��б�
    public Dictionary<int, LevelInfo> levelDictionary = new Dictionary<int, LevelInfo>();//�ؿ��ֵ����ڼ���
}
