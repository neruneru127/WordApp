using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortDropDown : MonoBehaviour
{
    public IUIManager uiManager;
    [HideInInspector]
    public SORT_TYPE sortType;

    [SerializeField]
    private SORT_TYPE initializeSortType;

    public enum SORT_TYPE
    {
        ALPHABET,
        CORRECT_PARSENT,
        DATE
    }

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
        sortType = initializeSortType;
        this.gameObject.GetComponent<Dropdown>().value = (int)sortType;
    }

    public void OnValueChanged(int selectedVal)
    {
        sortType = (SORT_TYPE)selectedVal;
        uiManager.SortExecute();
    }
}
