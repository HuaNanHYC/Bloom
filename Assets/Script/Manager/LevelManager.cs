using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        SetCurrentLevel(currentLevelId+1);
        SceneManageSystem.Instance.GoToFigureScene("Dialogue");
    }
    public void CurrentLevel()//���ü��ر���
    {
        SceneManageSystem.Instance.GoToFigureScene("Level" + (currentLevelId - 30000).ToString());
    }
    #endregion

    #region ��������

    [Header("������飬�����һ��ʧ�ܲ���ľ����")]
    public Dialogue dialogue1;//��һ�����˵ľ���
    public Dialogue dialogue2;//���һ�رض�ʧ�ܵľ���
    public int specialDialogueIndex;//��ʱ����Ҫ�������ʱ����һ��
    public void StartSpecialDialogue()
    {
        if(specialDialogueIndex == 1)
        {
            DialogueManager.Instance.StartDialogue(dialogue1);
        }
        else if (specialDialogueIndex == 2)
        {
            DialogueManager.Instance.StartDialogue(dialogue2);
        }
    }
    [System.Serializable]
    public struct DialogueInfo
    {
        public int levelId;
        public string[] startDialoguePath;
        public string[] endDialoguePath;
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
        public int StartDialogueIndex { get => startDialogueIndex; }
        public int EndDialogueIndex { get => endDialogueIndex;}
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
    public void DialogueAfterBlack()//����ת�������Ի�����ʱ�ã�dialogue�ĳ�������һ��onenable�͵���һ�������Ի�������
    {
        SceneManageSystem.Instance.GoToFigureScene("Dialogue");
    }
    #endregion
}
