using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager_QuizResult : IUIManager
{
    public GameObject scrollContentObject;
    public Scrollbar scrollBar;
    public GameObject wordResult;
    public GameManager gameManager;
    public SortOrderButton sortOrderButton;
    public SortDropDown sortDropDown;

    private List<WordData> wordDataList;

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
    public void ToActive(List<WordData> wordDataList)
    {
        this.gameObject.SetActive(true);
        this.wordDataList = wordDataList;
   
        foreach (Transform transform in scrollContentObject.transform)
        {
            Destroy(transform.gameObject);
        }


        foreach (var wordData in wordDataList.OrderBy(e => e.Word))
        {
            var wordResultObject = Instantiate(wordResult, Vector3.zero,
                Quaternion.identity, scrollContentObject.transform) as GameObject;

            var wordResultComponent = wordResultObject.gameObject.GetComponent<WordResult>();
            wordResultComponent.SetText(wordData.Word,
                wordData.Translate, wordData.CorrectParsent.ToString() + "%");
            wordResultComponent.WordID = wordData.ID;
        }
    }


    public void ToInactive()
    {
        this.gameObject.SetActive(false);
    }

    public void BackToTitle()
    {
        gameManager.ReturnTitle();
    }

    public override void SortExecute()
    {
        var orderDictionary = new List<WordData>();

        for (int i = 0; i < scrollContentObject.transform.childCount; i++)
        {
            var wordID =
                scrollContentObject.transform.GetChild(i).gameObject
                .GetComponent<WordResult>().WordID;
            var wordData = wordDataList.Where(e => e.ID.Equals(wordID)).First();

            orderDictionary.Add(wordData);
        }

        switch (sortDropDown.sortType)
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

        foreach (var item in orderDictionary)
        {
            int index = 0;
            for (; index < scrollContentObject.transform.childCount; index++)
            {
                var wordID =
                    scrollContentObject.transform.GetChild(index).gameObject
                    .GetComponent<WordResult>().WordID;

                if (item.ID.Equals(wordID))
                {
                    break;
                }

            }
            scrollContentObject.transform.GetChild(index).transform.SetAsFirstSibling();
        }
    }
}
