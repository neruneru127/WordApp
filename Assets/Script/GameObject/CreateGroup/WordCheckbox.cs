using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordCheckbox : MonoBehaviour
{
    public Image icon;
    public Sprite unCheckedIcon;
    public Sprite checkedIcon;
    public bool isChecked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        isChecked = false;
        icon.sprite = unCheckedIcon;
    }

    public void OnClicked()
    {
        isChecked = !isChecked;

        if (isChecked)
        {
            icon.sprite = checkedIcon;
        } 
        else
        {
            icon.sprite = unCheckedIcon;
        }
        
    }

    public void SetCheckState(bool checkState)
    {
        isChecked = checkState;

        if (isChecked)
        {
            icon.sprite = checkedIcon;
        }
        else
        {
            icon.sprite = unCheckedIcon;
        }
    }
}
