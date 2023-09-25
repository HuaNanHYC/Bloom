using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private static BulletManager instance;
    public static BulletManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        InitializeDictionary();
    }
    [System.Serializable]
    public struct BulletInfo
    {
        public int ID;
        public GameObject prefab;
    }
    [SerializeField]
    public List<BulletInfo> bulletList = new List<BulletInfo>();//�ӵ��б�
    public Dictionary<int, GameObject> bulletDictionary = new Dictionary<int, GameObject>();//�ӵ��ֵ����ڼ���
    public void InitializeDictionary()
    {
        for(int i=0;i<bulletList.Count; i++)
        {
            bulletDictionary.Add(bulletList[i].ID, bulletList[i].prefab);
        }
    }//��ʼ���ֵ�

    public Bullet currentBullet;//����ѡ����ӵ�

    public Dictionary<int, Bullet> loadBulletDictionary = new Dictionary<int, Bullet>();//�洢����װ���ӵ���˳��
    public GameObject revolver;//�������������֣�������һ��hole�ĸ�����
    public void LoadBullet()//�ӵ�װ������ֵ䣬��ȷ����ť
    {
        int holeNumber=1;
        while (holeNumber <= revolver.transform.childCount)
        { 
            for (int i = 0; i < revolver.transform.childCount; i++)
            {
                if (revolver.transform.GetChild(i).GetComponent<BulletHole>().number==holeNumber)
                {
                    Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                    if (loadBulletDictionary.ContainsKey(holeNumber)) loadBulletDictionary[holeNumber] = bullet;//�ֵ����о��滻
                    else loadBulletDictionary.Add(holeNumber, bullet);//�ֵ�û�о����
                    holeNumber++;
                    continue;
                }
                //��ֵ�ж�
                if (loadBulletDictionary.ContainsKey(holeNumber)) loadBulletDictionary[holeNumber] = null;//�ֵ����о��滻
                else loadBulletDictionary.Add(holeNumber, null);//�˴�Ӧ��װ����ӵ�����
                holeNumber++;
                continue;
            }
        }
    }
}
