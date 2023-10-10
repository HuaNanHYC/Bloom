using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DialogueSystem;
public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(this);

        InitializeLevelDictionary();
        InitializeDialogueDic();
    }
    #region �ؿ�����

    public int currentLevelId;
    public bool lastLevelJudge;//���һ�صĵ�һ�α����ж�
    [System.Serializable]
    public struct LevelInfo
    {
        public int levelID;
        //public string targetScene;
        public int enemyID;
        [Header("�̶�����id��λ����Ҫ��Ӧ")]
        public int[] steadyBulletID;//�̶��ӵ�����id
        public int[] steadyBulletPosition;//�̶��ӵ�λ��
        public int[] ableBulletID;//��Ҳ������ӵ�
    }
    [SerializeField]
    public List<LevelInfo> levelList = new List<LevelInfo>();//�ؿ��б�
    public Dictionary<int, LevelInfo> levelDictionary = new Dictionary<int, LevelInfo>();//�ؿ��ֵ����ڼ���

    
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
    public void NextLevel()//������ȥ����һ��,����������ת
    {
        if (currentLevelId + 1 >= 30009)
        {
            UIManager.Instance.LoadScene("StartAndEnd");
            return;
        }
        SetCurrentLevel(currentLevelId+1);
        SceneManageSystem.Instance.GoToFigureScene("Dialogue");
    }
    public void CurrentLevel()//���ü��ر���
    {
        if_StartDialogue = false;
        SceneManageSystem.Instance.GoToFigureScene("Level" + (currentLevelId - 30000).ToString());
    }
    #endregion

    #region ��������

    /*[Header("������飬�����һ��ʧ�ܲ���ľ����,�ֶ���ק����")]
    public Dialogue dialogue1;//��һ�����˵ľ���
    private bool if_Dialogue1Show;//��һ����ľ����Ƿ��Ѿ����Ź�
    public Dialogue dialogue8;//���һ�رض�ʧ�ܵľ���
    private bool if_Dialogue8Show;//�ڰ˹���ľ����Ƿ��Ѿ����Ź�*/
    public void LastLevelDialogue()//���һ�ص�����Ի��ж�
    {
        if (lastLevelJudge&&currentLevelId==30008)
        {
            if(StartSpecialDialogue())
                return;
        }
    }
    public bool StartSpecialDialogue()
    {
        DialogueInfo dialogueInfo = dialogueDic[currentLevelId];
        Dialogue dialogue = Resources.Load<Dialogue>(dialogueInfo.specialDialoguePath);
        bool if_HaveShow = dialogueInfo.If_SpecialDialogueShow;
        if (dialogue != null&&!if_HaveShow)
        {
            DialogueManager.Instance.StartDialogue(dialogue);
            GameObject.FindWithTag("UiDialogue").transform.GetChild(0).gameObject.SetActive(true);

            dialogueInfo.IfSpecialDialogueShow(true);
            dialogueDic[currentLevelId] = dialogueInfo;
            return true;
        }
        return false;
    }
    [System.Serializable]
    public struct DialogueInfo
    {
        public int levelId;
        public string[] startDialoguePath;
        public string[] endDialoguePath;
        public string specialDialoguePath;
        private bool if_SpecialDialogueShow;//��������Ƿ񲥷Ź�
        private int startDialogueIndex;
        private int endDialogueIndex;
        public void StartIndexAdd()
        {
            this.startDialogueIndex += 1;
        }
        public void EndIndexAdd()
        {
            this.endDialogueIndex += 1;
        }
        public void IfSpecialDialogueShow(bool setting)
        {
            this.if_SpecialDialogueShow = setting;
        }
        public int StartDialogueIndex { get => startDialogueIndex; }
        public int EndDialogueIndex { get => endDialogueIndex;}
        public bool If_SpecialDialogueShow { get => if_SpecialDialogueShow;}
    }
    [SerializeField]
    public List<DialogueInfo> dialogueList = new List<DialogueInfo>();//�ؿ��б�
    public Dictionary<int, DialogueInfo> dialogueDic = new Dictionary<int, DialogueInfo>();//�ؿ��ֵ䷽�����
    public void InitializeDialogueDic()//�ֵ��ʼ��
    {
        for(int i=0;i<dialogueList.Count;i++)
        {
            dialogueDic.Add(dialogueList[i].levelId, dialogueList[i]);
        }
    }
    private bool if_StartDialogue = true;//�Ƿ��ǿ�ʼ���飬���򲥽�������
    public bool If_StartDialogue { get => if_StartDialogue; set => if_StartDialogue = value; }
    public Dialogue NextDialogueInStartDialogue()//������һ�ζԻ���û�оͿ��Խ���ؿ���
    {
        int index = dialogueDic[currentLevelId].StartDialogueIndex;
        if (index < dialogueDic[currentLevelId].startDialoguePath.Length)
        {
            Dialogue dialogue = Resources.Load<Dialogue>(dialogueDic[currentLevelId].startDialoguePath[index]);
            //�޸��ֵ����ֵ
            DialogueInfo info = dialogueDic[currentLevelId];
            info.StartIndexAdd();
            dialogueDic[currentLevelId] = info;
            return dialogue;
        }
        else
        {
            if_StartDialogue = false;
            CurrentLevel();//�Ի�û�ˣ����뱾��
            return null;
        }
    }
    public Dialogue NextDialogueInEndDialogue()//������һ�ζԻ���û�оͿ�����һ�ؿ���
    {
        int index = dialogueDic[currentLevelId].EndDialogueIndex;
        if (index < dialogueDic[currentLevelId].endDialoguePath.Length)
        {
            Dialogue dialogue = Resources.Load<Dialogue>(dialogueDic[currentLevelId].endDialoguePath[index]);

            DialogueInfo info = dialogueDic[currentLevelId];
            info.EndIndexAdd();
            dialogueDic[currentLevelId] = info;

            return dialogue;
        }
        else
        {
            if_StartDialogue=true;//�´ξ��ǿ�ʼ������
            NextLevel();//�Ի�û�ˣ�������һ��
            return null;
        }
    }
    private bool continueSprite;//ͣ��ͼƬ�ж�
    public bool ContinueSprite { get { return continueSprite; } set { continueSprite = value; } }



    public void DialogueAfterBlack()//����ת�������Ի�����ʱ�ã�dialogue�ĳ�������һ��onenable�͵���һ�������Ի�������
    {
        SceneManageSystem.Instance.GoToFigureScene("Dialogue");
    }
    public void DialogueNoBlack()
    {
        continueSprite = true;
        SceneManager.LoadScene("Dialogue");
    }
    #endregion

    //��ͷ�ͽ�β��Ƶ����
    [SerializeField]
    private bool startVideoPlay;
    private bool endVideoPlay;
    public bool StartVideoPlay { get => startVideoPlay; set => startVideoPlay = value; }
    public bool EndVideoPlay { get => endVideoPlay; set => endVideoPlay = value; }

}
