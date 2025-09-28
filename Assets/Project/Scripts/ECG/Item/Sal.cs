using DG.Tweening;
using UnityEngine;

public class Sal : Item, IUsable
{
    [SerializeField] private GameObject saltCirclePrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnOffset = 0.05f;
    [SerializeField] AudioData salDropAudio;

    private void Start()
    {
        
    }
    
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
� � � � � � // Llama al nuevo m�todo para agregar los puntos al gestor
            ritualManager.AddRitualPoints(instantiatedCircle);
            // El RitualManager ahora puede manejar su propio contador de pasos
� � � � � � ritualManager.MarkUsedItemStepComplete(); // Esto podr�as dejarlo en AddRitualPoints

            FadeAnimation();
� � � � }

        // El inventario elimina el objeto de sal
        Inventory inventory = user.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.DeleteItem(this.gameObject);
        }

    }
    private void FadeAnimation()
    {
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();
        Debug.Log("iniciando");
        if (renderer == null) return;
        Debug.Log("Empezando Animacion");
        // Get a single reference to the material
        Material material = renderer.material;

        // Get the original color to fade to
        Color startColor = material.color;

        // Create the invisible color with 0 alpha
        Color invisibleColor = startColor;
        invisibleColor.a = 0;

        // Set the initial color to invisible
        material.SetColor("_Color", invisibleColor);

        // Animate the color to the final color
        material.DOColor(startColor, 1.0f);
    }
}
