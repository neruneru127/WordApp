using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortOrderButton : MonoBehaviour
{
    public IUIManager uiManager;
    public Image icon;
    // true = è∏èá, false = ç~èá
    public bool isOrder = true;

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
        isOrder = true;
        icon.gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
    }

    public void OnClicked()
    {
        icon.gameObject.transform.rotation 
            = Quaternion.Euler(0, 0, icon.gameObject.transform.rotation.eulerAngles.z * -1);
        isOrder = !isOrder;

        uiManager.SortExecute();
    }
}
