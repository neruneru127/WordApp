using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager_Title : IUIManager
{
    public GameObject selectGroupDialog;
    public GameManager gameManager;

    private List<WordData> wordDataList;
    private List<WordGroupData> groupList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToActive(List<WordData> wordDataList, List<WordGroupData> groupList)
    {
        this.gameObject.SetActive(true);
        HideSelectGroupDialog();

        this.wordDataList = wordDataList;
        this.groupList = groupList;

    }

    public void ToInactive()
    {
        this.gameObject.SetActive(false);
    }


    public void ShowSelectGroupDialog()
    {
        selectGroupDialog.gameObject.SetActive(true);
        selectGroupDialog.GetComponent<SelectGroupDialog>().SetGourpData(groupList, true);
    }

    public void SelectedGroupOfQuiz(string groupName)
    {
        gameManager.SendTitleToQuiz(groupName);
    }

    public void HideSelectGroupDialog()
    {
        selectGroupDialog.SetActive(false);
    }

    public void CreateGroup()
    {
        selectGroupDialog.gameObject.SetActive(true);
        selectGroupDialog.GetComponent<SelectGroupDialog>().SetGourpData(groupList, false);


    }

    public void SelectedGroupOfEdit(string groupName)
    {
        gameManager.SendTitleToCreateGroup(groupName);
    }

    public override void SortExecute()
    {
        throw new System.NotImplementedException();
    }
}
