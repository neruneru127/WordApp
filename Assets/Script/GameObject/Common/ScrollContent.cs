using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    public IUIManager uiManager;
    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IUIManager GetuiManager()
    {
        return uiManager;
    }

    public GameManager GetGameManager()
    {
        return gameManager;
    }
}
