using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public UIManager_Title uiManager_Title;
    public UIManager_Quiz uiManager_Quiz;
    public UIManager_QuizResult uiManager_QuizResult;
    public UIManager_CreateGroup uiManager_CreateGroup;
    public UIManager_Menu uIManager_Menu;


    private List<WordData> wordDataList;
    private List<WordGroupData> groupList;

    private AudioSource audioSource;

    public static float VoiceVolume
    {
        get; set;
    }
    public static float SEVolume
    {
        get; set;
    }


    private List<WordData> WordDataList
    {
        get
        {
            return new List<WordData>(wordDataList);
        }
        set
        {
            wordDataList = new List<WordData>(value);
        }
    }

    private List<WordGroupData> GroupList
    {
        get
        {
            return new List<WordGroupData>(groupList);
        }
        set
        {
            groupList = new List<WordGroupData>(value);
        }
    }

    private void AddGroupList(WordGroupData groupData)
    {
        if (groupList.Contains(groupData))
        {
            groupList.Remove(groupData);
        }
        groupList.Add(groupData);
    }

    private void RemoveGroupList(WordGroupData groupData)
    {
        groupList.Remove(groupData);
    }


    // Start is called before the first frame update
    void Start()
    {
        WordDataList = WordDataAccesser.Read();
        GroupList = GroupDataAccesser.Read();
        var configData = ConfigAccesser.Read();
        VoiceVolume = configData.VoiceVolume;
        SEVolume = configData.SEVolume;

        uIManager_Menu.SetInitialVolume(VoiceVolume, SEVolume);
        audioSource = GetComponent<AudioSource>();

        ReturnTitle();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClickAudio()
    {
        audioSource.volume = SEVolume;
        audioSource.Play();
    }

    public void SendTitleToQuiz(string groupName)
    {
        var selectedGroupData = GroupList.Where(e =>
            e.GroupName.Equals(groupName)).First();

        var instantWordDataList = new List<WordData>();
        foreach (var wordID in selectedGroupData.WordList)
        {
            instantWordDataList.Add(WordDataList.Where(e =>
                e.ID.Equals(wordID)).First().CreateInitializedCopy());
        }


        uiManager_Title.ToInactive();
        uiManager_Quiz.ToActive(instantWordDataList, selectedGroupData);
    }

    public void SendTitleToCreateGroup(string groupName)
    {
        uiManager_Title.ToInactive();
        
        if (Init.CREATE_GROUP_STR.Equals(groupName))
        {
            uiManager_CreateGroup.ToActive(WordDataList, null);
        }
        else
        {
            uiManager_CreateGroup.ToActive(WordDataList,
                GroupList.Where(e => e.GroupName.Equals(groupName)).First());
        }

    }

    public void SendCreateGroupToTitle(WordGroupData groupData, bool removeFlg)
    {
        if (groupData != null)
        {
            if (removeFlg)
            {
                RemoveGroupList(groupData);
            }
            else
            {
                AddGroupList(groupData);
            }

            WriteGroupData();
        }

        uiManager_CreateGroup.ToInactive();
        uiManager_Title.ToActive(WordDataList, GroupList);
    }

    public void SendQuizToQuizResult(List<WordData> instantWordDataList)
    {
        uiManager_Quiz.ToInactive();
        uiManager_QuizResult.ToActive(instantWordDataList);

        foreach(var instantData in instantWordDataList)
        {
            var originData = 
                WordDataList.Where(e => e.ID.Equals(instantData.ID)).First();

            originData.MergeScore(instantData.TotalCnt, instantData.IncorrectCnt);
        }

        WriteWordData();
    }


    /// <summary>
    /// 強制的にタイトルへ戻る用
    /// 
    /// </summary>
    public void ReturnTitle()
    {
        uiManager_Title.ToActive(WordDataList, GroupList);
        uiManager_Quiz.ToInactive();
        uiManager_CreateGroup.ToInactive();
        uIManager_Menu.ToInactive();
        uiManager_QuizResult.ToInactive();
    }

    public void WriteWordData()
    {
        WordDataAccesser.Write(WordDataList);
    }


    public void WriteGroupData()
    {
        GroupDataAccesser.Write(GroupList);
    }

    public void WriteConfig()
    {
        var configData = new ConfigData();
        configData.VoiceVolume = VoiceVolume;
        configData.SEVolume = SEVolume;
        ConfigAccesser.Write(configData);
    }

}
