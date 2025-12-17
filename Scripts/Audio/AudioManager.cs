using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterParam", volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicParam", volume);
    }
    
    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("UISFXParam", volume);
    }
    
    public void SetGameSoundVolume(float volume)
    {
        audioMixer.SetFloat("GameSoundsParam", volume);
    }
}
