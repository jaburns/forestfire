using UnityEngine;
using System.Collections.Generic;

static public class Sounds
{
    static AudioSource _sounder;
    readonly static Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    static public void Play(string name)
    {
        AudioClip clip = null;

        if (_clips.ContainsKey(name)) {
            clip = _clips[name];
        } else {
            clip = Resources.Load(name) as AudioClip;
            _clips[name] = clip;
        }

        if (_sounder == null) {
            var go = new GameObject();
            go.name = "SOUNDS SOURCE";
            _sounder = go.AddComponent<AudioSource>();
        }

        _sounder.PlayOneShot(clip);
    }
}
