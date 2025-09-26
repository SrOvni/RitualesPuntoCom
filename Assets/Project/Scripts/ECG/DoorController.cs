using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private InputActionReference mouseLookAction;

    private Rigidbody rb;
    private bool isBeingHeld = false;
    private Transform playerCamera;

    
    private float interactionSide;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10f;
    }

    void OnEnable()
    {
        mouseLookAction.action.Enable();
    }

    void OnDisable()
    {
        mouseLookAction.action.Disable();
    }

    public string GetInteractText()
    {
        return "Arrastrar puerta";
    }

    public void Interact(GameObject interactor)
    {
        isBeingHeld = true;
        playerCamera = interactor.GetComponentInChildren<Camera>().transform;

        // Calculamos de qué lado de la puerta está el jugador UNA SOLA VEZ.

        Vector3 doorFaceDirection = transform.up;
        Vector3 playerDirection = (playerCamera.position - transform.position).normalized;

        float dot = Vector3.Dot(doorFaceDirection, playerDirection);

        // Guardamos el resultado (1 para en frente, -1 para detrás) en nuestra variable.
        interactionSide = Mathf.Sign(dot);
    }

    public void StopInteraction()
    {
        isBeingHeld = false;
    }

    void FixedUpdate()
    {
        if (isBeingHeld)
        {
            Vector2 mouseDelta = mouseLookAction.action.ReadValue<Vector2>();
            float mouseX = mouseDelta.x;

            // El eje de rotación (la bisagra) es el eje Z local
            Vector3 rotationAxis = transform.forward;

            
            rb.AddTorque(rotationAxis * mouseX * sensitivity * interactionSide, ForceMode.VelocityChange);
        }
    }
}
