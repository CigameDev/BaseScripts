using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLevel : MonoBehaviour
{
    [HideInInspector] public int LevelIndex;
    private Button button;
    private Text textLevel;
    void Awake()
    {
        button = GetComponent<Button>();
        textLevel = this.gameObject.transform.GetChild(0).GetComponent<Text>();
    }
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (LevelIndex <= SaveData.GetLevelUnlock() && LevelIndex <= 10)//moi co 10 level
            {
                SaveData.SaveLevelCurrent(LevelIndex);
                SceneManager.LoadScene(1);//SceneGamePlay
            }
        });
    }
    public void SetText()
    {
        textLevel.text = LevelIndex.ToString();
    }
}
