using UnityEngine;

public class Sal : Item, IUsable
{
    [SerializeField] private GameObject saltCirclePrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnOffset = 0.05f;
    [SerializeField] AudioData salDropAudio;
    public void Use(GameObject user)
    {
        GameObject instantiatedCircle = null;

        if (saltCirclePrefab != null)
        {
            Ray ray = new Ray(user.transform.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, groundLayer))
            {
                Vector3 spawnPosition = hit.point + new Vector3(0, spawnOffset, 0);
                instantiatedCircle = Instantiate(saltCirclePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {
                Vector3 spawnPosition = user.transform.position + new Vector3(0, -1f + spawnOffset, 0);
                instantiatedCircle = Instantiate(saltCirclePrefab, spawnPosition, Quaternion.identity);
            }
        }

        
        AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

        if (audioPlayer != null && salDropAudio != null)
        {
            audioPlayer.Play(salDropAudio);
        }
        else
        {
            Debug.Log($"No se pudo reproducir el audio {salDropAudio} en {audioPlayer} ");
        }

        RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
        if (ritualManager != null && instantiatedCircle != null)
        {
            // Llama al nuevo método para agregar los puntos al gestor
            ritualManager.AddRitualPoints(instantiatedCircle);
            // El RitualManager ahora puede manejar su propio contador de pasos
            ritualManager.MarkUsedItemStepComplete(); // Esto podrías dejarlo en AddRitualPoints
        }

        // El inventario elimina el objeto de sal
        Inventory inventory = user.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.DeleteItem(this.gameObject);
        }
    }
}
