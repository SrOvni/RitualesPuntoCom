using UnityEngine;

public class Fusibles : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Fusibles";
    [SerializeField] private AudioData interactSound;
    public string GetInteractText()
    {
        return interactText;
    }
    public void Interact(GameObject interactor)
    {
        if (LightManager.Instance != null)
        {
            AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

            if (audioPlayer != null && interactSound != null)
            {
                audioPlayer.Play(interactSound);
            }
            else
            {
                Debug.Log($"No se pudo reproducir el audio {interactSound} en {audioPlayer} ");
            }

            LightManager.Instance.ToggleLights();
        }
    }
}
