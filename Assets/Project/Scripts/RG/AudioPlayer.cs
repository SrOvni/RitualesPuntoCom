using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class AudioPlayer : Singleton<AudioPlayer>
{
    List<AudioSource> audioSources;
    void Awake()
    {
        audioSources = new(5)
        {
            gameObject.AddComponent<AudioSource>(),
            gameObject.AddComponent<AudioSource>(),
            gameObject.AddComponent<AudioSource>(),
            gameObject.AddComponent<AudioSource>(),
            gameObject.AddComponent<AudioSource>(),
        };
        audioSources.ForEach(a => a.playOnAwake = false);
    }
    public void Play(AudioData data)
    {
        var audioSource = audioSources.FirstOrDefault(a => a.clip == null);
        if (audioSource == null)
        {
            audioSources.Add(gameObject.AddComponent<AudioSource>());
            audioSource = audioSources.Last();
        }
        audioSource.clip = data.Clip;
        audioSource.pitch = data.Pitch;
        audioSource.loop = data.Loop;
        audioSource.volume = data.Volume;
        audioSource.PlayDelayed(data.Delay);
        StartCoroutine(ReleaseAudioSource(audioSource));


    }
    IEnumerator ReleaseAudioSource(AudioSource audio)
    {
        yield return new WaitUntil(() => !audio.isPlaying);
        audio.clip = null;
    }
}
