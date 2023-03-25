using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;
    [SerializeField] private AudioSource sfxAus;
    [SerializeField] private AudioSource musicAus;
    [Range(0f, 1f)]
    [SerializeField] private float volumeSfxDefault = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float volumeMusicDefault = 1f;
    public AudioClip backGroundMusic;
    public List<Sound> AllSound;
    private void Awake()
    {
        
        if(_instance==null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }  
        else
        {
            Destroy(gameObject);
        }

        PlayMusic(backGroundMusic,true);
    }
   
    public void PlaySound(SoundType soundType)
    {
        Sound sound = GetSound(soundType);
        float volume = volumeSfxDefault * SaveData.GetVolumeSound();//nhan voi gia tri cua thanh slider neu co
        sfxAus.PlayOneShot(sound.audioClip, volume);
    }    

    private Sound GetSound(SoundType type)
    {
        return AllSound.Find(x=>x.typeSound == type);
    }    

    private void PlayMusic(AudioClip music ,bool loop)
    {
        if(musicAus !=null && music!=null)
        {
            musicAus.clip = music;
            musicAus.loop = loop;
            musicAus.volume = volumeMusicDefault * SaveData.GetVolumeMusic();//nhan them voi gia tri thanh slider
            musicAus.Play();
        }    
    }    

    public void StopMusic()
    {
        if(musicAus)
        {
            musicAus.Stop();
        }    
    }    
}
public enum SoundType
{
    Win,Lose,Sniper,Button_Click
}

[System.Serializable]
public class Sound
{
    public string nameSound;
    public SoundType typeSound;
    public AudioClip audioClip;
}
