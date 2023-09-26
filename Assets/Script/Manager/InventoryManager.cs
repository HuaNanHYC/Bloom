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
    public Dictionary<int, int> ownBulletDictionary = new Dictionary<int, int>();//����ӵ�е��ӵ�����ӵ�е�����
    public List<int> ownBulletList = new List<int>();//����id�ж�
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

    #region �������Ҳ���ϵ�����,���㱣��





    #endregion



    #region ����������Եķ���
    private string PLAYER_DATA_FILE_NAME = "PlayerData.GameSave";


    [SerializeField]
    class PlayerSave//���ڱ������
    {
        public struct InventorySave
        {
            public int id;
            public int amount;
        }
        public List<InventorySave> inventoryList = new List<InventorySave>();//�б��洢id����Ӧ����


        //����������Զ�Ӧ����



    }
    PlayerSave playerSave()//��ұ��������Ա���
    {
        PlayerSave playerDataSave = new PlayerSave();
        foreach (KeyValuePair<int,int> ownBullet in ownBulletDictionary)
        {
            PlayerSave.InventorySave inventorySave=new PlayerSave.InventorySave();
            inventorySave.id = ownBullet.Key;
            inventorySave.amount = ownBullet.Value;
            playerDataSave.inventoryList.Add(inventorySave);
        }


        //���������Ա���




        return playerDataSave;
    }
    public void SavePlayerData()//����
    {
        if (SaveSystem.SaveByJson(PLAYER_DATA_FILE_NAME, playerSave(), Application.persistentDataPath))
        {
            Debug.Log($"����ɹ�{Application.persistentDataPath}");
        }
    }

    #endregion
}
