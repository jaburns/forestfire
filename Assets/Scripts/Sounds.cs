using UnityEngine;
using System.Collections.Generic;

static public class Sounds
{
    readonly static Dictionary<string, float> VOLUMES = new Dictionary<string, float> {
        { "explosion", 1 }
      , { "base", 1.5f }
    };

    static AudioSource _sounder;
    readonly static Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    static AudioSource _waterLooper;

    static public void Play(string name)
    {
        AudioClip clip = null;
        float volume = 1f;

        if (_clips.ContainsKey(name)) {
            clip = _clips[name];
        } else {
            clip = Resources.Load(name) as AudioClip;
            _clips[name] = clip;
        }

        if (VOLUMES.ContainsKey(name)) {
            volume = VOLUMES[name];
        }

        if (_sounder == null) {
            var go = new GameObject();
            go.name = "SOUNDS SOURCE";
            _sounder = go.AddComponent<AudioSource>();
            _sounder.volume = 0.4f;
        }

        _sounder.PlayOneShot(clip, volume);
    }

    static public void SetWaterLoop(bool on)
    {
        if (_waterLooper == null) {
            var go = new GameObject();
            go.name = "WATER LOOPER";
            _waterLooper = go.AddComponent<AudioSource>();
            _waterLooper.loop = true;
            _waterLooper.clip = Resources.Load("water") as AudioClip;
            _waterLooper.volume = 0.1f;
        }

        if (on && !_waterLooper.isPlaying) {
            _waterLooper.Play();
        } else if (!on && _waterLooper.isPlaying) {
            _waterLooper.Stop();
        }
    }
}
