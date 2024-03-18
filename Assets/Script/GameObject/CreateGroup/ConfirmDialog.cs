using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour
{
    public UIManager_CreateGroup uiManager;
    public TextMeshProUGUI header;
    public TMP_InputField inputField;
    public TextMeshProUGUI applyButton;
    public TextMeshProUGUI cancelButton;

    private DIALOG_TYPE dialogType;

    public enum DIALOG_TYPE
    {
        OK,
        OK_CANCEL,
        YES_NO
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToActive(DIALOG_TYPE dialogType, string text, string inputFieldText)
    {
        inputField.text = inputFieldText;
        this.gameObject.SetActive(true);

        this.dialogType = dialogType;
        header.text = text;
        switch (dialogType)
        {
            case DIALOG_TYPE.OK:
                cancelButton.text = "OK";
                applyButton.transform.parent.gameObject.SetActive(false);
                cancelButton.transform.parent.transform.localPosition = new Vector3(0, -100.0f, 0);
                inputField.gameObject.SetActive(false);
                break;

            case DIALOG_TYPE.OK_CANCEL:
                applyButton.text = "OK";
                applyButton.transform.parent.gameObject.SetActive(true);
                cancelButton.text = "Cancel";
                cancelButton.transform.parent.transform.localPosition = new Vector3(87, -100.0f, 0);
                inputField.gameObject.SetActive(true);
                break;

            case DIALOG_TYPE.YES_NO:
                applyButton.text = "Yes";
                applyButton.transform.parent.gameObject.SetActive(true);
                cancelButton.text = "No";
                cancelButton.transform.parent.transform.localPosition = new Vector3(87, -100.0f, 0);
                inputField.gameObject.SetActive(false);
                break;
        }

    }

    public void ToInactive()
    {
        this.gameObject.SetActive(false);
    }

    public void OKButtonOnClicked()
    {
        if (dialogType.Equals(DIALOG_TYPE.YES_NO))
        {
            uiManager.DeleteGroup();
        }
        else
        {
            uiManager.CreateGroup(inputField.text);
        }
    }

    public void CancelButtonOnClicked()
    {
        ToInactive();
    }
}
