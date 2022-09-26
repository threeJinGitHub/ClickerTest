using System;
using UnityEngine;

public class SaveCallback : MonoBehaviour
{
    public Action OnSaveData;
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            OnSaveData?.Invoke();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            OnSaveData?.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        OnSaveData?.Invoke();
    }
}
