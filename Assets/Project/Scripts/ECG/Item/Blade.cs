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
    // --- Variables para la animaci�n de la navaja ---
    [Header("Animaci�n de Corte")]
    [Tooltip("El desplazamiento hacia adelante para simular el corte.")]
    [SerializeField] private Vector3 sliceOffset = new Vector3(0, 0, 0.1f); // Peque�o movimiento hacia adelante
    [Tooltip("La duraci�n de la animaci�n de corte.")]
    [SerializeField] private float sliceDuration = 0.1f;
    [Tooltip("Retraso antes de que la navaja regrese a su posici�n original.")]
    [SerializeField] private float returnDelay = 0.05f;

    // Guardar la posici�n local original de la navaja
    private Vector3 originalLocalPosition;

    private void Awake()
    {
        
        // Guarda la posici�n local original del cuchillo para la animaci�n
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

        // 2. Reproducir el efecto de part�culas
        if (instantiatedParticles != null)
        {
            // Mover el efecto a la posici�n actual del usuario antes de reproducirlo
            instantiatedParticles.transform.position = user.transform.position;
            instantiatedParticles.transform.rotation = user.transform.rotation;
            instantiatedParticles.Play();
        }

        usesRemaining--;

        // Comprobar si el �tem ya no tiene usos
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
                // Llama al nuevo m�todo para agregar los puntos al gestor
                ritualManager.AddRitualPoints(instantiatedCircle);
                // El RitualManager ahora puede manejar su propio contador de pasos
                ritualManager.MarkUsedItemStepComplete(); // Esto podr�as dejarlo en AddRitualPoints

            }
            // Eliminar el �tem del inventario
            Inventory inventory = user.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.DeleteItem(this.gameObject);
            }
        }
        else
        {
            
            Debug.Log($"Usos restantes para este �tem: {usesRemaining}");
        }
    }

    private void AnimateSlice()
    {
        // Mueve la navaja hacia adelante y luego la regresa a su posici�n original
        transform.DOLocalMove(originalLocalPosition + sliceOffset, sliceDuration)
            .SetEase(Ease.OutSine) // Un easing suave para el movimiento de salida
            .OnComplete(() =>
            {
                // Espera un momento y luego regresa la navaja a su posici�n original
                transform.DOLocalMove(originalLocalPosition, sliceDuration)
                    .SetEase(Ease.InSine) // Un easing suave para el retorno
                    .SetDelay(returnDelay); // A�ade un peque�o retraso antes de regresar
            });
    }
}
