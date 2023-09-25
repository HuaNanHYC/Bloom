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
    private void Start()
    {
        CreateBulletSelect();
    }
    [System.Serializable]
    public struct BulletInfo
    {
        public BulletType type;
        public GameObject prefab;
    }
    [SerializeField]
    public List<BulletInfo> bulletList = new List<BulletInfo>();//�ӵ��б�
    public Dictionary<BulletType, GameObject> bulletDictionary = new Dictionary<BulletType, GameObject>();//�ӵ��ֵ����ڼ���
    public void InitializeDictionary()
    {
        for(int i=0;i<bulletList.Count; i++)
        {
            bulletDictionary.Add(bulletList[i].type, bulletList[i].prefab);
        }
    }//��ʼ���ֵ�

    public Bullet currentBullet;//����ѡ����ӵ�
    #region �����������ȡ
    public Dictionary<int, Bullet> loadedBulletDictionary = new Dictionary<int, Bullet>();//�洢����װ���ӵ���˳��
    [Header("�����鿴�������ӵ��Է�����Ե��б�")]
    public List<Bullet> loadedBulletList = new List<Bullet>();
    [Header("���ֵ�ui")]
    public GameObject revolver;//�������������֣�������һ��hole�ĸ�����
    public void LoadBullet()//�ӵ�װ������ֵ䣬��ȷ����ť
    {
        loadedBulletList.Clear();
        int holeNumber=1;
        while (holeNumber <= revolver.transform.childCount)
        { 
            for (int i = 0; i < revolver.transform.childCount; i++)
            {
                if (revolver.transform.GetChild(i).GetComponent<BulletHole>().number==holeNumber)
                {
                    Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                    if (loadedBulletDictionary.ContainsKey(holeNumber)) loadedBulletDictionary[holeNumber] = bullet;//�ֵ����о��滻
                    else loadedBulletDictionary.Add(holeNumber, bullet);//�ֵ�û�о����
                    holeNumber++;
                    continue;
                }
                //�յ�ϻ�жϣ�װ��հ���
                Bullet nullBullet = bulletDictionary[BulletType.NullBullet].GetComponent<Bullet>();
                if (loadedBulletDictionary.ContainsKey(holeNumber)) loadedBulletDictionary[holeNumber] = nullBullet;//�ֵ����о��滻
                else loadedBulletDictionary.Add(holeNumber, nullBullet);//�˴�Ӧ��װ����ӵ�����
                holeNumber++;
                continue;
            }
        }
        //���Կɲ鿴���б�
        foreach(Bullet bullet in loadedBulletDictionary.Values)
        {
            loadedBulletList.Add(bullet);
        }

    }
    #endregion

    #region �ӵ�ѡ������ȡ���������ӵ�
    [Header("ѡ���ӵ���ui����")]
    public GameObject bulletSelect;
    /// <summary>
    /// �����ӵ�ѡ���б�
    /// </summary>
    public void CreateBulletSelect()
    {
        ClearBulletSelect();
        InventoryManager.Instance.CheckOwnType();//�ȼ��ӵ�е�����
        List<BulletType> list = InventoryManager.Instance.ownBulletList;
        //������������ȥ�����ֵ�
        for (int i = 0; i < list.Count; i++)
        {
            //�����ֵ����Ӧ����ӵ�е�������������
            for (int j = 0; j < InventoryManager.Instance.ownBulletDictionary[list[i]]; j++)
            {
                Instantiate(bulletDictionary[list[i]], bulletSelect.transform);
            }
        }
        
    }
    public void ClearBulletSelect()//���һ���ӵ�ѡ�����
    {
        for(int i=0;i<bulletSelect.transform.childCount;i++)
        {
            Destroy(bulletSelect.transform.GetChild(i).gameObject);
        }
    }

    #endregion
}
