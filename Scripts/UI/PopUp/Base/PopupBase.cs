using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum NamePopups
{
    Win = 0,
    Lose,
    LevelMenu,
}
public class PopupBase : MonoBehaviour
{
    public AudioClip audioClipShowPopup;
    public AudioClip audioClipClosePopup;
    public string message;
    public NamePopups Namepopup { get; private set; }
    protected int Priority { get; private set; }
    public virtual NamePopups GetNamePopup()
    {
        return Namepopup;
    }
    public virtual int GetPriority()
    {
        if (Priority > 0)
        {
            return Priority;
        }
        else
        {
            return 0;
        }
    }
    public virtual void Show()
    {
        if (gameObject != null)
        {
            gameObject.SetActive(true);
            //if (SoundManager.Instance != null && this.audioClipShowPopup != null)
            //{
            //    SoundManager.Instance.PlaySound(this.audioClipShowPopup);
            //}
        }
    }
    public virtual void Hide()
    {
        if (gameObject != null && gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            DestroyPopup();
        }
        else
        {
            return;
        }
    }
    protected virtual void DestroyPopup()
    {
        if (gameObject != null && !gameObject.activeInHierarchy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }
}