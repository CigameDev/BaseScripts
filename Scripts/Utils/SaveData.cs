using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static int GetLevelCurrent()
    {
        return PlayerPrefs.GetInt(GameConst.Level_Current, 1);
    }
    public static void SaveLevelCurrent(int level)
    {
        PlayerPrefs.SetInt(GameConst.Level_Current, level);
    }
    public static int GetLevelUnlock()
    {
        return PlayerPrefs.GetInt(GameConst.Level_Unlock, 1);
    }
    public static void SaveLevelUnlock(int level)
    {
        PlayerPrefs.SetInt(GameConst.Level_Unlock, level);
    }

    public static bool GetStateSoundCurrent()
    {
        int stateSound = PlayerPrefs.GetInt(GameConst.Sound_Current, 1);//ban dau thi trang thai se la bat nhac
        if (stateSound == 0) return false;
        return true;
    }
    public static void SaveStateSoundCurrent(bool stateSound)
    {
        if (stateSound)
        {
            PlayerPrefs.SetInt(GameConst.Sound_Current, 1);
        }
        else PlayerPrefs.SetInt(GameConst.Sound_Current, 0);
    }

    public static bool GetStateMusicCurrent()
    {
        int stateMusic = PlayerPrefs.GetInt(GameConst.Music_Current, 1);//ban dau thi trang thai se la bat nhac
        if (stateMusic == 0) return false;
        return true;
    }
    public static void SaveStateMusicCurrent(bool stateMusic)
    {
        if (stateMusic)
        {
            PlayerPrefs.SetInt(GameConst.Music_Current, 1);
        }
        else PlayerPrefs.SetInt(GameConst.Music_Current, 0);
    }
    public static float GetVolumeSound()
    {
        return PlayerPrefs.GetFloat(GameConst.Sound_Volume, 1f);
    }
    public static void SaveVolumeSound(float volume)
    {
        PlayerPrefs.SetFloat(GameConst.Sound_Volume, volume);
    }
    public static float GetVolumeMusic()
    {
        return PlayerPrefs.GetFloat(GameConst.Music_Volume, 1f);
    }
    public static void SaveVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat(GameConst.Music_Volume, volume);
    }
}


