using UnityEngine;

public class LightManager : MonoBehaviour
{

    public static LightManager Instance { get; private set; }

    [SerializeField] private GameObject lightParent;

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
    }

    public void TurnOffLights()
    {
        if (lightParent != null)
        {
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
}
