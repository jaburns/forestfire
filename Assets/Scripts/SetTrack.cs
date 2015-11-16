using UnityEngine;
using System.Collections;

public class SetTrack : MonoBehaviour {
    public MusicManager.trackType sceneTrack = MusicManager.trackType.title;
    
    void Start () {
        MusicManager.instance.SetTrack(sceneTrack);
	}
	
}
