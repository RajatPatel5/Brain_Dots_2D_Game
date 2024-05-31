using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource Source;
    public Sound[] clips;
    public void Awake()
    {
        instance = this;
    }

    //public bool IsMusicMute => Bg_Source.mute;
    public bool IsSoundMute => Source.mute;

    public void PlaySound(SoundName name)
    {
        foreach (var item in clips)
        {
            if (item.name == name)
            {
                Source.PlayOneShot(item.clip);
                break;
            }
        }
    }

    [System.Serializable]
    public class Sound
    {
        public SoundName name;
        public AudioClip clip;
    }

    public enum SoundName
    {
        Level_Completed,
        GameOver

    }
}

