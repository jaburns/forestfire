using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public static MusicManager instance = null;
    public AudioClip titleTrack;
    public AudioClip gameTrack;
    public AudioClip endingTrack;

    AudioSource audioSource = null;

    public enum trackType { title, game, win};

    trackType currentTrack;
    trackType trackToPlay;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
	
    public void SetTrack(trackType track)
    {
        switch (track)
        {
            case trackType.title:
                playTrack(titleTrack);
                break;
            case trackType.game:
                playTrack(gameTrack, audioSource.time);
                break;
            case trackType.win:
          //      playTrack(endingTrack);
                break;
            default:
                break;
        }
    }

    void playTrack(AudioClip clip, float offset = 0)
    {
        if (clip == null)
            return;
        audioSource.clip = clip;
        audioSource.time = offset;
        audioSource.Play();
    }	
}
