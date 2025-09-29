using UnityEngine;

public class LightManager : MonoBehaviour
{

    public static LightManager Instance { get; private set; }

    [SerializeField] private GameObject lightParent;

    [SerializeField] private AudioData powerDownSound;

    public bool IsOn { get; private set; }
    private void Awake()
    {
        // Si ya hay una instancia, destruye este nuevo objeto para asegurar que solo haya una.
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Si no hay una instancia, esta se convierte en la única instancia.
            Instance = this;
            
        }

        if (lightParent != null)
        {
            IsOn = lightParent.activeSelf;
        }
    }

    public void TurnOffLights()
    {
        if (lightParent != null)
        {
            AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

            if (audioPlayer != null && powerDownSound != null)
            {
                audioPlayer.Play(powerDownSound);
            }
            else
            {
                Debug.Log($"No se pudo reproducir el audio {powerDownSound} en {audioPlayer} ");
            }
            lightParent.SetActive(false);
            Debug.Log("Las luces han sido apagadas.");
        }
        else
        {
            Debug.LogWarning("No se asignó un objeto padre de luces.");
        }
    }

    public void TurnOnLights()
    {
        if (lightParent != null)
        {
            lightParent.SetActive(true);
            Debug.Log("Las luces han sido encendidas.");
        }
        else
        {
            Debug.LogWarning("No se asignó un objeto padre de luces.");
        }
    }

    public void ToggleLights()
    {
        if (lightParent != null)
        {
            // Invierte el estado de las luces
            IsOn = !IsOn;
            lightParent.SetActive(IsOn);

            if (IsOn)
            {
                Debug.Log("Las luces han sido encendidas.");
            }
            else
            {
                Debug.Log("Las luces han sido apagadas.");
            }
        }
        else
        {
            Debug.LogWarning("No se asignó un objeto padre de luces.");
        }
    }
}
