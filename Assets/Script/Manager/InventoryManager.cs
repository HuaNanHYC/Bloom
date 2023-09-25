using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
        DontDestroyOnLoad(gameObject);
    }
    public Dictionary<int, int> ownBulletDictionary = new Dictionary<int, int>();//ӵ�е�����
    public List<int> ownBulletList = new List<int>();
    /// <summary>
    /// ����ӵ��ķ���
    /// </summary>
    /// <param name="bullet"></param>
    public void AddBullet(int bullet)
    {
        if (ownBulletDictionary.ContainsKey(bullet)) ownBulletDictionary[bullet]++;
        else ownBulletDictionary.Add(bullet, 1);
    }//����ӵ����Ե����������
        
    public void CheckOwnType()//����ֵ���ӵ�е�type����Щ�����ڱ����ֵ�Ȼ����ӽ��ӵ�ѡ��ҳ��
    {
        ownBulletList.Clear();//�������һ�ε�
        foreach(int bulletType in ownBulletDictionary.Keys)
        {
            if (ownBulletList.Contains(bulletType)) continue;
            ownBulletList.Add(bulletType);
        }
    }
}
