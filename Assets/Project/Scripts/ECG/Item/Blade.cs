using UnityEngine;

public class Blade : Item, IUsable
{
    [SerializeField] AudioData bladeUseAudio;


    public void Use(GameObject user)
    {
        AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

        if (audioPlayer != null && bladeUseAudio != null)
        {
            audioPlayer.Play(bladeUseAudio);
        }
        else
        {
            Debug.Log($"No se pudo reproducir el audio {bladeUseAudio} en {audioPlayer} ");
        }

        RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
        if (ritualManager != null )
        {
            // El RitualManager ahora puede manejar su propio contador de pasos
            ritualManager.MarkUsedItemStepComplete(); 
          
        }

        Inventory inventory = user.GetComponent<Inventory>();
        if (inventory != null)
        {
<<<<<<< Updated upstream
            inventory.DeleteItem(this.gameObject);
=======
            // Mover el efecto a la posición actual del usuario antes de reproducirlo
            instantiatedParticles.transform.position = user.transform.position;
            instantiatedParticles.transform.rotation = user.transform.rotation;
            instantiatedParticles.Play();
>>>>>>> Stashed changes
        }
    }
}
