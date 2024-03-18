using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGroupDialog : MonoBehaviour
{
    public ScrollContent scrollContentObject;
    public GameObject groupSelect;

    public UIManager_Title uiManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetGourpData(List<WordGroupData> groupDataList, bool isSelectMode)
    {
        foreach (Transform transform in scrollContentObject.transform)
        {
            Destroy(transform.gameObject);
        }

        foreach (var groupData in groupDataList)
        {
            var groupSelectObject = Instantiate(groupSelect, Vector3.zero,
                Quaternion.identity, scrollContentObject.transform) as GameObject;
            groupSelectObject.GetComponent<GroupSelect>().SetText(groupData.GroupName, isSelectMode);
        }

        if (!isSelectMode)
        {
            var groupSelectObject = Instantiate(groupSelect, Vector3.zero,
                Quaternion.identity, scrollContentObject.transform) as GameObject;
            groupSelectObject.GetComponent<GroupSelect>().SetText(Init.CREATE_GROUP_STR, isSelectMode);
        }
    }


}
