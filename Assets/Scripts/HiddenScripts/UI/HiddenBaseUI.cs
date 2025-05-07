using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HiddenBaseUI : MonoBehaviour
{
    protected HiddenUIManager uiManager;

    public virtual void Init(HiddenUIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    protected abstract HiddenUIState GetUIState();
    
    public void SetActive(HiddenUIState state)
    {
        this.gameObject.SetActive(GetUIState() == state) ;
    }
}
