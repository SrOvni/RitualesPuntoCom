using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    [Header("Headbob Settings")]
    public float bobFrequency = 1.5f;        // Frecuencia del movimiento
    public float bobHorizontalAmplitude = 0.1f; // Amplitud horizontal
    public float bobVerticalAmplitude = 0.1f;   // Amplitud vertical
    public float bobSmoothness = 2f;          // Suavizado del movimiento

    [Header("Headbob Activation")]
    public float velocityThreshold = 0.5f;    // Velocidad mínima para activar
    public bool enableHeadbob = true;         // Activar/desactivar headbob

    private float bobTimer = 0f;
    private Vector3 originalLocalPosition;
    private Vector3 targetBobPosition;
    
    // Rigidbody rigidbody;
    private PlayerController playerController;

    void Start()
    {
        // Guardar la posición local original de la cámara
        originalLocalPosition = transform.localPosition;

        // Intentar obtener referencias a los componentes necesarios
        // rigidbody = GetComponentInParent<Rigidbody>();
        playerController = GetComponentInParent<PlayerController>();
        
        // Inicializar la posición objetivo
        targetBobPosition = originalLocalPosition;
    }

    void Update()
    {
        if (!enableHeadbob) return;

        if (IsPlayerMoving())
        {
            // Calcular el movimiento de headbob
            bobTimer += Time.deltaTime * bobFrequency;
            
            // Calcular las oscilaciones
            float horizontalBob = Mathf.Sin(bobTimer) * bobHorizontalAmplitude;
            float verticalBob = (Mathf.Sin(bobTimer * 2) * 0.5f + 0.5f) * bobVerticalAmplitude;
            
            // Aplicar el movimiento
            targetBobPosition = originalLocalPosition + new Vector3(horizontalBob, verticalBob, 0);
        }
        else
        {
            // Volver suavemente a la posición original
            bobTimer = 0f;
            targetBobPosition = originalLocalPosition;
        }

        // Aplicar suavizado al movimiento
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetBobPosition, bobSmoothness * Time.deltaTime);
    }

    private bool IsPlayerMoving()
    {
        if (playerController != null)
        {
            return playerController.Input.Direction.magnitude > 0;
        }
        else
        {
            return false;
        }
    }

    // Métodos públicos para controlar el headbob
    public void EnableHeadbob() => enableHeadbob = true;
    public void DisableHeadbob() => enableHeadbob = false;
    public void ToggleHeadbob() => enableHeadbob = !enableHeadbob;

    // Resetear la posición cuando se desactiva
    private void OnDisable()
    {
        transform.localPosition = originalLocalPosition;
    }
}