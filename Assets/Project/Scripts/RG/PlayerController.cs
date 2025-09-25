using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    
    [Header("Rotation Limits")]
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;
    
    private Rigidbody rb;
    [SerializeField] private InputReader input;
    public InputReader Input => input;
    CharacterController characterController;
    private float verticalRotation = 0f; // Para almacenar la rotación vertical acumulada
    [SerializeField] AudioData[] startingAudio;
    private void Awake() {
        input.EnablePlayerInputActions();
        characterController = GetComponent<CharacterController>();
        //AudioPlayer.Instance.Play(startingAudio[0]);
    }
    void Start()
    {
        //AudioPlayer.Instance.Play(startingAudio[1]);
        
        // rb = GetComponent<Rigidbody>();
        
        // Bloquear y ocultar el cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void FixedUpdate()
    {
        characterController.SimpleMove(HandleMovement());
        HandleRotation();
    }
    
    Vector3 HandleMovement()
    {
        Vector3 moveDirection = transform.forward * input.Direction.y * moveSpeed;
        // También puedes añadir movimiento lateral si lo necesitas
        moveDirection += transform.right * input.Direction.x * moveSpeed;

        return moveDirection;
    }
    
    void HandleRotation()
    {
        // Rotación horizontal (Yaw) - gira todo el objeto
        float horizontalRotateAmount = input.LookDirection.x * mouseSensitivity;
        Quaternion horizontalTurn = Quaternion.Euler(0f, horizontalRotateAmount, 0f);
        
        // Rotación vertical (Pitch) - solo gira la cámara (o la cabeza)
        float verticalRotateAmount = -input.LookDirection.y * mouseSensitivity; // Negativo para invertir el eje Y del mouse
        
        // Acumular y limitar la rotación vertical
        verticalRotation += verticalRotateAmount;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        
        // Aplicar rotaciones
        transform.Rotate(horizontalTurn.eulerAngles);
        
        // Si tienes una cámara separada para la vista en primera persona
        ApplyVerticalRotationToCamera();
    }
    
    void ApplyVerticalRotationToCamera()
    {
        // Buscar la cámara hijo (asumiendo que la cámara es hijo del objeto con Rigidbody)
        Camera playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.transform.localEulerAngles = new Vector3(verticalRotation, 0f, 0f);
        }
    }
    
    void OnDestroy()
    {
        // Restaurar el cursor cuando el objeto se destruye
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}