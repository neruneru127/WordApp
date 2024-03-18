using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GroupSelect : MonoBehaviour
{
    public TextMeshProUGUI textMP;

    private UIManager_Title uiManager;
    private GameManager gameManager;
    private string groupName;
    private bool isSelectMode;

    // Start is called before the first frame update
    void Start()
    {
        var scrollContent = this.transform.parent.GetComponent<ScrollContent>();
        uiManager = (UIManager_Title)scrollContent.GetuiManager();
        gameManager = scrollContent.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string groupName, bool isSelectMode)
    {
        this.groupName = groupName;
        textMP.SetText(groupName);
        this.isSelectMode = isSelectMode;
    }

    public void OnClicked()
    {
        gameManager.PlayClickAudio();
        if (isSelectMode)
        {
            uiManager.SelectedGroupOfQuiz(groupName);
        }
        else
        {
            uiManager.SelectedGroupOfEdit(groupName);
        }

    }

    public void PlayClickAudio()
    {
        gameManager.PlayClickAudio();
    }
}
