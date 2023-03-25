using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPopUp : PopupBase
{
    public const NamePopups namePopups = NamePopups.LevelMenu;

    [SerializeField] private Button btnHome;

    [SerializeField] private Button btnPass;
    [SerializeField] private Button btnUnlock;
    [SerializeField] private Button btnLock;
    [SerializeField] private Transform content;

    void Awake()
    {
        SpawnLevel();
    }
    void Start()
    {
        btnHome.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);//"StartScene" ho?c "HomeScene"
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
    
    private void SpawnLevel()
    {
        int levelUnlock = SaveData.GetLevelUnlock();
        for(int i=0;i<10;i++)
        {
            Button button = null;
            if (i + 1 < levelUnlock)
            {
                button = Instantiate(btnPass, content);
            }
            else if (i + 1 == levelUnlock)
            {
                button = Instantiate(btnUnlock, content);
            }
            else button = Instantiate(btnLock, content);
            button.GetComponent<ButtonLevel>().LevelIndex = i + 1;
            button.GetComponent<ButtonLevel>().SetText();
        }    
    }    
}
