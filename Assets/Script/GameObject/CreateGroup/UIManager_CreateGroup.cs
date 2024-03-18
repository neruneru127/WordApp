using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_CreateGroup : IUIManager
{

    public GameObject scrollContentObject;
    public Scrollbar scrollBar;
    public GameObject wordSelect;
    public GameManager gameManager;
    public SortOrderButton sortOrderButton;
    public SortDropDown sortDropDown;
    public ConfirmDialog confirmDialog;
    public GameObject deleteButton;


    private List<WordData> wordDataList;
    private WordGroupData groupData;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        scrollBar.value = 1;
    }

    /// <summary>
    /// 
    /// 
    /// </summary>
    /// <param name="wordDataList">単語一覧</param>
    /// <param name="groupData">問題データ. nullを指定した場合、新規作成.</param>
    public void ToActive(List<WordData> wordDataList, WordGroupData groupData)
    {
        this.gameObject.SetActive(true);
        confirmDialog.ToInactive();
        this.wordDataList = wordDataList;
        this.groupData = groupData;

        if (groupData == null)
        {
            deleteButton.SetActive(false);
        }
        else
        {
            deleteButton.SetActive(true);
        }

        foreach (Transform transform in scrollContentObject.transform)
        {
            Destroy(transform.gameObject);
        }

        var checkedWordList = new List<int>();
        if (groupData != null)
        {
            checkedWordList = groupData.WordList;
        }

        foreach (var wordData in wordDataList.OrderBy(e => e.Word))
        {
            var wordSelectObject = Instantiate(wordSelect, Vector3.zero,
                Quaternion.identity, scrollContentObject.transform) as GameObject;

            var wordSelectComponent = wordSelectObject.gameObject.GetComponent<WordSelect>();
            wordSelectComponent.SetText(wordData.Word,
                wordData.Translate, wordData.CorrectParsent.ToString() + "%");
            wordSelectComponent.WordID = wordData.ID;

            if(checkedWordList.Contains(wordData.ID))
            {
                wordSelectObject.GetComponentInChildren<WordCheckbox>()
                    .SetCheckState(true);
            }
        }
    }

    public void ToInactive()
    {
        this.gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        gameManager.SendCreateGroupToTitle(null, false);
    }

    public override void SortExecute()
    {
        var orderDictionary = new List<WordData>();

        for(int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordID =
                scrollContentObject.transform.GetChild(i).gameObject
                .GetComponent<WordSelect>().WordID;
            var wordData = wordDataList.Where(e => e.ID.Equals(wordID)).First();

            orderDictionary.Add(wordData);
        }

        switch(sortDropDown.sortType)
        {
            case SortDropDown.SORT_TYPE.ALPHABET:
                if (!sortOrderButton.isOrder)
                {
                    orderDictionary = orderDictionary
                        .OrderBy(e => e.Word).ToList();
                }
                else
                {
                    orderDictionary = orderDictionary
                        .OrderByDescending(e => e.Word).ToList();
                }
                break;

            case SortDropDown.SORT_TYPE.CORRECT_PARSENT:
                if (!sortOrderButton.isOrder)
                {
                    orderDictionary = orderDictionary
                        .OrderBy(e => e.CorrectParsent).ToList();
                }
                else
                {
                    orderDictionary = orderDictionary
                        .OrderByDescending(e => e.CorrectParsent).ToList();
                }
                break;

            case SortDropDown.SORT_TYPE.DATE:
                if (!sortOrderButton.isOrder)
                {
                    orderDictionary = orderDictionary
                        .OrderBy(e => e.CreateDateTime).ToList();
                }
                else
                {
                    orderDictionary = orderDictionary
                        .OrderByDescending(e => e.CreateDateTime).ToList();
                }
                break;
        }

        foreach(var item in orderDictionary)
        {
            int index = 0;
            for (; index < scrollContentObject.transform.childCount; index++)
            {
                var wordID =
                    scrollContentObject.transform.GetChild(index).gameObject
                    .GetComponent<WordSelect>().WordID;

                if (item.ID.Equals(wordID))
                {
                    break;
                }

            }
            scrollContentObject.transform.GetChild(index).transform.SetAsFirstSibling();
        }
    }

    public void ApplyClicked()
    {
        int checkedCnt = 0;
        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordCheckbox =
                scrollContentObject.transform.GetChild(i)
                .GetComponentInChildren<WordCheckbox>();

            if (wordCheckbox.isChecked)
            {
                checkedCnt++;
            }
        }

        if (checkedCnt < 4)
        {
            confirmDialog.ToActive(ConfirmDialog.DIALOG_TYPE.OK, "4つ以上選択してください", "");
        }
        else
        {
            confirmDialog.ToActive(ConfirmDialog.DIALOG_TYPE.OK_CANCEL,
                "名前を入力してください" , groupData == null ? "" : groupData.GroupName);
        }

    }


    public void CreateGroup(string groupName)
    {
        var wordDataList = new List<int>();

        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordSelect = scrollContentObject.transform
                .GetChild(i).GetComponent<WordSelect>();
            var wordCheckbox =
                scrollContentObject.transform.GetChild(i)
                .GetComponentInChildren<WordCheckbox>();

            if (wordCheckbox.isChecked)
            {
                var wordID = wordSelect.WordID;

                wordDataList.Add(wordID);
            }
        }

        var wordGroupData = new WordGroupData();
        wordGroupData.WordList = wordDataList;
        wordGroupData.GroupName = groupName;

        gameManager.SendCreateGroupToTitle(wordGroupData, false);
    }

    public void DeleteGroup()
    {
        gameManager.SendCreateGroupToTitle(groupData, true);
    }

    public void DeleteButtonOnClicked()
    {
        confirmDialog.ToActive(ConfirmDialog.DIALOG_TYPE.YES_NO, "問題を削除しますか?", "");
    }

    private void CheckeStateReset()
    {
        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordSelectObject = scrollContentObject.transform.GetChild(i);
            wordSelectObject.GetComponentInChildren<WordCheckbox>().SetCheckState(false);
        }

    }

    public void RandomModeOnClicked()
    {
        CheckeStateReset();

        var numList = new List<int>();
        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            numList.Add(i);
        }

        var random = new System.Random();
        for (int i = 0; numList.Count > 0; i++)
        {

            if (i >= Init.DEFAULT_WORD_CNT)
            {
                break;
            }

            var index = random.Next(0, numList.Count);

            scrollContentObject.transform.GetChild(numList[index])
                .GetComponentInChildren<WordCheckbox>().SetCheckState(true);

            numList.RemoveAt(index);

        }
    }

    public void WeakModeOnClicked()
    {
        CheckeStateReset();
        var orderDictionary = new Dictionary<int, WordData>();

        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordSelectObject = scrollContentObject.transform.GetChild(i);

            var wordID = wordSelectObject.gameObject.GetComponent<WordSelect>().WordID;
            var wordData = wordDataList.Where(e => e.ID.Equals(wordID)).First();

            orderDictionary.Add(i, wordData);
        }

        orderDictionary = orderDictionary.OrderBy(e => e.Value.CorrectParsent)
            .ToDictionary(e => e.Key, e=> e.Value);

        int j = 0;

        foreach (var itemNum in orderDictionary.Keys)
        {
            scrollContentObject.transform.GetChild(itemNum)
                .GetComponentInChildren<WordCheckbox>().SetCheckState(true);

            j++;
            if (j >= Init.DEFAULT_WORD_CNT)
            {
                break;
            }

        }
    }

    public void NotAnswerdModeOnClicked()
    {
        CheckeStateReset();

        int checkCnt = 0;

        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordSelectObject = scrollContentObject.transform.GetChild(i);

            var wordID = wordSelectObject.gameObject.GetComponent<WordSelect>().WordID;
            var wordData = wordDataList.Where(e => e.ID.Equals(wordID)).First();

            if (wordData.TotalCnt == 0)
            {
                wordSelectObject.GetComponentInChildren<WordCheckbox>().SetCheckState(true);
                checkCnt++;

                if (checkCnt >= Init.DEFAULT_WORD_CNT)
                {
                    break;
                }
            }

        }
    }
}
