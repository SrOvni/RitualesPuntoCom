using DG.Tweening;
using UnityEngine;

public class Blade : Item, IUsable
{
    [SerializeField] private GameObject pentagramPrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnOffset = 0.05f;

    [SerializeField] private GameObject bloodParticlePrefab;
    private ParticleSystem instantiatedParticles;
    [SerializeField] AudioData bladeUseAudio;
    [SerializeField] private int maxUses = 7;
    private int usesRemaining;
    // --- Variables para la animación de la navaja ---
    [Header("Animación de Corte")]
    [Tooltip("El desplazamiento hacia adelante para simular el corte.")]
    [SerializeField] private Vector3 sliceOffset = new Vector3(0, 0, 0.1f); // Pequeño movimiento hacia adelante
    [Tooltip("La duración de la animación de corte.")]
    [SerializeField] private float sliceDuration = 0.1f;
    [Tooltip("Retraso antes de que la navaja regrese a su posición original.")]
    [SerializeField] private float returnDelay = 0.05f;

    // Guardar la posición local original de la navaja
    private Vector3 originalLocalPosition;

    private void Awake()
    {
        
        // Guarda la posición local original del cuchillo para la animación
        originalLocalPosition = transform.localPosition;
    }

    private void Start()
    {
        usesRemaining = maxUses;
    }

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
        if (instantiatedParticles == null)
        {
            // Creamos una instancia del prefab y guardamos la referencia al componente ParticleSystem
            GameObject particlesInstance = Instantiate(bloodParticlePrefab, user.transform.position, Quaternion.identity);
            instantiatedParticles = particlesInstance.GetComponent<ParticleSystem>();
        }

        // 2. Reproducir el efecto de partículas
        if (instantiatedParticles != null)
        {
            // Mover el efecto a la posición actual del usuario antes de reproducirlo
            instantiatedParticles.transform.position = user.transform.position;
            instantiatedParticles.Play();
        }

        usesRemaining--;

        // Comprobar si el ítem ya no tiene usos
        if (usesRemaining <= 0)
        {
            GameObject instantiatedCircle = null;

            if (pentagramPrefab != null)
            {
                Ray ray = new Ray(user.transform.position, Vector3.down);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 10f, groundLayer))
                {
                    Vector3 spawnPosition = hit.point + new Vector3(0, spawnOffset, 0);
                    instantiatedCircle = Instantiate(pentagramPrefab, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Vector3 spawnPosition = user.transform.position + new Vector3(0, -1f + spawnOffset, 0);
                    instantiatedCircle = Instantiate(pentagramPrefab, spawnPosition, Quaternion.identity);
                }
            }

            RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
            if (ritualManager != null && instantiatedCircle != null)
            {
                // Llama al nuevo método para agregar los puntos al gestor
                ritualManager.AddRitualPoints(instantiatedCircle);
                // El RitualManager ahora puede manejar su propio contador de pasos
                ritualManager.MarkUsedItemStepComplete(); // Esto podrías dejarlo en AddRitualPoints

            }
            // Eliminar el ítem del inventario
            Inventory inventory = user.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.DeleteItem(this.gameObject);
            }
        }
        else
        {
            
            Debug.Log($"Usos restantes para este ítem: {usesRemaining}");
        }
    }

    private void AnimateSlice()
    {
        // Mueve la navaja hacia adelante y luego la regresa a su posición original
        transform.DOLocalMove(originalLocalPosition + sliceOffset, sliceDuration)
            .SetEase(Ease.OutSine) // Un easing suave para el movimiento de salida
            .OnComplete(() =>
            {
                // Espera un momento y luego regresa la navaja a su posición original
                transform.DOLocalMove(originalLocalPosition, sliceDuration)
                    .SetEase(Ease.InSine) // Un easing suave para el retorno
                    .SetDelay(returnDelay); // Añade un pequeño retraso antes de regresar
            });
    }
}
