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
        bulletList.Clear();
        CreateBulletSelect();
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
    #region �����������ȡ
    public Dictionary<int, Bullet> loadedBulletDictionary = new Dictionary<int, Bullet>();//�洢����װ���ӵ���˳��
    [Header("�հ���id")]
    public int emptyBullet;
    [Header("�����鿴�������ӵ��Է�����Ե��б�")]
    public List<Bullet> loadedBulletList = new List<Bullet>();
    [Header("���ֵ�ui")]
    public GameObject revolver;//�������������֣�������һ��hole������
    public void LoadBullet()//�ӵ�װ������ֵ䣬��ȷ����ť
    {
        //����б��б����ɵ�������
        for (int i = bulletList.Count - 1; i >= 0; i--)
        {
            if (i<transform.childCount)
                Destroy(transform.GetChild(i));
        }
        loadedBulletList.Clear();
        
        int holeNumber=1;
        while (holeNumber <= revolver.transform.childCount)
        { 
            for (int i = 0; i < revolver.transform.childCount; i++)
            {
                if (revolver.transform.GetChild(i).GetComponent<BulletHole>().number==holeNumber)
                {
                    Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                    if (bullet == null) bullet = bulletDictionary[emptyBullet].GetComponent<Bullet>();//�յĻ����հ���
                    if (loadedBulletDictionary.ContainsKey(holeNumber)) loadedBulletDictionary[holeNumber] = bullet;//�ֵ����о��滻
                    else loadedBulletDictionary.Add(holeNumber, bullet);//�ֵ�û�о����
                    holeNumber++;
                    continue;
                }
                //�յ�ϻ�жϣ�װ��հ���
                Bullet nullBullet = bulletDictionary[emptyBullet].GetComponent<Bullet>();
                if (loadedBulletDictionary.ContainsKey(holeNumber)) loadedBulletDictionary[holeNumber] = nullBullet;//�ֵ����о��滻
                else loadedBulletDictionary.Add(holeNumber, nullBullet);//�˴�Ӧ��װ����ӵ�����
                holeNumber++;
                continue;
            }
        }
        //���Կɲ鿴���б�,ͬʱҪ�õ�
        foreach(Bullet bullet in loadedBulletDictionary.Values)
        {
            string name = bullet.bulletName;
            GameObject newBulletObject=new GameObject(name);
            newBulletObject.transform.SetParent(transform);
            newBulletObject.AddComponent<Bullet>();
            Bullet newBullet= newBulletObject.GetComponent<Bullet>();
            newBullet.actualDamage = bullet.actualDamage;
            newBullet.settingDamage = bullet.settingDamage;
            newBullet.ID = bullet.ID;
            newBullet.bulletName = bullet.bulletName;
            newBullet.If_OnlyUseBullet = true;
            loadedBulletList.Add(newBullet);
        }
        for(int i = 0; i < loadedBulletList.Count; i++)
        {
            loadedBulletList[i].BulletInHoleNumber = i;
        }

    }

    [Header("�ӵ���ͻui")]
    public GameObject putIn_Conflict;
    public bool JudgeExistTogether_IfCanPutIn(int id)//�ж��Ƿ����װ�����������޳�ͻ
    {
        if (id == 10005)//��Ҫ�ж��Ƿ���ż����������������
        {
            for(int i=0;i<revolver.transform.childCount;i++)
            {
                Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                if (bullet != null)
                {
                    if (bullet.ID == 10006 || bullet.ID == 10007)
                    {
                        PutInConflicStart();
                        return false;
                    }
                }
            }
            return true;
        }
        else if (id == 10006)//��Ҫ�ж��Ƿ���������������������
        {
            for (int i = 0; i < revolver.transform.childCount; i++)
            {
                Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                if (bullet != null)
                {
                    if (bullet.ID == 10005 || bullet.ID == 10007)
                    {
                        PutInConflicStart();
                        return false;
                    }
                }
            }
            return true;
        }
        else if (id == 10007)//��Ҫ�ж��Ƿ�����������ż��������
        {
            for (int i = 0; i < revolver.transform.childCount; i++)
            {
                Bullet bullet = revolver.transform.GetChild(i).GetComponent<BulletHole>().currentBullet;
                if (bullet != null)
                {
                    if (bullet.ID == 10005 || bullet.ID == 10006)
                    {
                        PutInConflicStart();
                        return false;
                    }
                }
            }
            return true;
        }

        return true;
    }
    public void PutInConflicStart()
    {
        StopAllCoroutines();
        putIn_Conflict.SetActive(false);
        StartCoroutine(PutInConflict());
    }
    IEnumerator PutInConflict()
    {
        putIn_Conflict.SetActive(true);
        yield return new WaitForSeconds(2);
        putIn_Conflict.SetActive(false);
        yield return null;
    }
    #endregion

    #region �ӵ�ѡ������ȡ�����ӵ������ӵ�
    [Header("ѡ���ӵ���ui����")]
    public GameObject bulletSelect;
    public void CreateBulletSelect()// �����ӵ�ѡ���б�
    {
        ClearBulletSelect();
        /*InventoryManager.Instance.CheckOwnType();//�ȼ��ӵ�е�����
        List<int> list = InventoryManager.Instance.ownBulletList;
        if (LevelManager.Instance != null)
            LevelManager.Instance.EnableBulletIdJudge(list);//ȥ���������ӵ�
        //������������ȥ�����ֵ�
        for (int i = 0; i < list.Count; i++)
        {
            //�����ֵ����Ӧ����ӵ�е�������������
            for (int j = 0; j < InventoryManager.Instance.ownBulletDictionary[list[i]]; j++)
            {
                Instantiate(bulletDictionary[list[i]], bulletSelect.transform);
            }
        }*/


        //���ݹؿ��ṩ�Ŀ����ӵ������ӵ�
        int currentLevel = LevelManager.Instance.currentLevelId;
        int[] bulletsToCreate = LevelManager.Instance.levelDictionary[currentLevel].ableBulletID;
        for (int i = 0; i < bulletsToCreate.Length; i++)
        {
            Instantiate(bulletDictionary[bulletsToCreate[i]], bulletSelect.transform);
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
