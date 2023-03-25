using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class WinPopUp : PopupBase
{
    public const NamePopups namePopups = NamePopups.Win;
    [SerializeField] Button btnHome;
    [SerializeField] Button btnRetry;
    [SerializeField] Button btnPlayNext;
    void Start()
    {
        btnHome.onClick.AddListener(() =>
        {
            Hide();
            int levelCurrent = SaveData.GetLevelCurrent();
            if (levelCurrent == SaveData.GetLevelUnlock())
            {
                SaveData.SaveLevelUnlock(levelCurrent + 1);
            }
            AllPopupHandle.Instance.InitPopup(NamePopups.LevelMenu);
        });
        btnRetry.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("GamePlay");//load lai sceneplay
        });
        btnPlayNext.onClick.AddListener(() =>
        {
            int levelCurrent = SaveData.GetLevelCurrent();
            if(levelCurrent == SaveData.GetLevelUnlock())
            {
                SaveData.SaveLevelUnlock(levelCurrent + 1);
            }    
            SaveData.SaveLevelCurrent(levelCurrent +1);

            if (levelCurrent == 10) SaveData.SaveLevelCurrent(1);//chi co 10 level nen quay lai level dau
            SceneManager.LoadScene("GamePlay");
        });
    }

    
   
    public override NamePopups GetNamePopup()
    {
        return namePopups;
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
}
