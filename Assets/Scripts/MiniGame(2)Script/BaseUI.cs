using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    protected UImanager uiManager;

    public virtual void Init(UImanager uiManager)
    {
        this.uiManager = uiManager;

    }

    protected abstract UIState GetUIState();

    public void SetActive(UIState state)
    {
        gameObject.SetActive(GetUIState() == state);
    }
}
