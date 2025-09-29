using UnityEngine;

public class Pollito : Item, IUsable
{
    [SerializeField] private AudioData eatAudio;
    public void Use(GameObject user)
    {
        AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

        if (audioPlayer != null && eatAudio != null)
        {
            audioPlayer.Play(eatAudio);
        }
        else
        {
            Debug.Log($"No se pudo reproducir el audio {eatAudio} en {audioPlayer} ");
        }
        DayOneManager.Instance.PlayerAte = true;

        LightManager.Instance.ToggleLights();
    }
}
