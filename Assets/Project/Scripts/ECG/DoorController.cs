using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    [SerializeField] private float openSpeed = 50f; // Increased for better feel with delta
    [SerializeField] private float sensitivity = 1.5f;
    [SerializeField] private InputActionReference mouseLookAction; // Reference to your mouse delta action

    private Rigidbody rb;
    private bool isBeingHeld = false;
    private Transform playerCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Ensure the door is on a layer your raycast can hit
        // gameObject.layer = LayerMask.NameToLayer("Interactable"); 
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
        // Player starts holding the door
        isBeingHeld = true;
        playerCamera = interactor.GetComponentInChildren<Camera>().transform;

        // We don't need to check for MouseButtonUp anymore, 
        // the PlayerInteractor handles starting the interaction.
        // We need a way to STOP the interaction.
    }

    // A new public method to stop the interaction
    public void StopInteraction()
    {
        isBeingHeld = false;
    }

    void FixedUpdate() // Switched to FixedUpdate for physics
    {
        if (isBeingHeld)
        {
            Debug.Log("Is being Held");


            //// Read mouse delta from the new Input System
            //Vector2 mouseDelta = mouseLookAction.action.ReadValue<Vector2>();

            //// We'll primarily use the vertical movement for push/pull
            //float mouseY = mouseDelta.y;
            //float mouseX = mouseDelta.x;

            //// Calculate the direction the player is pushing/pulling from
            //Vector3 playerViewPoint = playerCamera.position;
            //Vector3 doorPoint = rb.worldCenterOfMass; 

            //// The point where the player is "looking" at the door
            //RaycastHit hit;
            //if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 5f))
            //{
            //    if (hit.rigidbody == rb)
            //    {
            //        doorPoint = hit.point;
            //    }
            //}

            //// Apply force based on vertical mouse movement
            //Vector3 forceDirection = playerCamera.forward;

            //Vector3 sideDirection = playerCamera.right * mouseX;
            //// Calcular la dirección de empuje/tirón (hacia adelante/atrás)
            //Vector3 pushDirection = playerCamera.forward * mouseY;

            //// Combinar ambas direcciones para un control más completo
            //Vector3 totalForceDirection = (pushDirection + sideDirection).normalized;

            //rb.AddForceAtPosition(forceDirection * mouseX * openSpeed * Time.fixedDeltaTime, doorPoint, ForceMode.Force);





            // Leer el delta del mouse desde el nuevo Input System
            Vector2 mouseDelta = mouseLookAction.action.ReadValue<Vector2>();

            // Usaremos SOLAMENTE el movimiento horizontal del mouse (mouseX)
            float mouseX = mouseDelta.x;

            Vector3 rotationAxis = transform.forward;

            // Para que el control se sienta intuitivo, necesitamos saber de qué lado de la puerta está el jugador.
            // Usamos el producto punto (Dot Product) para esto.
            Vector3 doorForward = transform.forward; // La dirección "hacia afuera" de la puerta
            Vector3 playerDirection = (playerCamera.position - transform.position).normalized;

            // Si el producto punto es positivo, el jugador está en frente. Si es negativo, está detrás.
            // Esto nos permite invertir el control para que siempre se sienta natural.
            float dot = Vector3.Dot(doorForward, playerDirection);

            // Aplicamos una fuerza de rotación (Torque) alrededor del eje Y (transform.up)
            // Multiplicamos por el signo del producto punto para que el control se invierta correctamente.
            rb.AddTorque(rotationAxis * mouseX * sensitivity * Mathf.Sign(dot), ForceMode.VelocityChange);
        }
    }
}
