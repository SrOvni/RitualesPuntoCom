using UnityEngine;

[CreateAssetMenu(fileName = "SFXData")]
public class AudioData : ScriptableObject
{
    public AudioClip Clip;
    [Range(0, 100)]public float Volume = 50;
    public bool Loop = false;
    public float Pitch = 1;
    public float Delay = 0;
}
