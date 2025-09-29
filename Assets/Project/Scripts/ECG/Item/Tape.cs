using UnityEngine;

public class Tape : Item, IUsable
{

    [SerializeField] private AudioData clothUseAudio;
    public void Use(GameObject user)
    {

        AudioPlayer audioPlayer = FindAnyObjectByType<AudioPlayer>();

        if (audioPlayer != null && clothUseAudio != null)
        {
            audioPlayer.Play(clothUseAudio);
        }
        else
        {
            Debug.Log($"No se pudo reproducir el audio {clothUseAudio} en {audioPlayer} ");
        }

        RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
        if (ritualManager != null && clothUseAudio != null)
        {
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
