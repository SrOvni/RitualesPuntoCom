using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;
    private Inventory inventory;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference interactAction;
    [SerializeField] private InputActionReference dropAction;
    [SerializeField] private InputActionReference grabAction;

    private IInteractable currentTarget;
    private IInteractable activeInteractable;


    void OnEnable()
    {

        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
        interactAction.action.canceled += OnInteractCanceled;

        grabAction.action.Enable();
        grabAction.action.performed += OnGrab;
        grabAction.action.canceled += OnGrabCanceled;

        dropAction.action.Enable();
        dropAction.action.performed += OnDrop;
    }

    void OnDisable()
    {
        grabAction.action.performed -= OnGrab;
        grabAction.action.canceled -= OnGrabCanceled;
        grabAction.action.Disable();

        dropAction.action.performed -= OnDrop;
        dropAction.action.Disable();

        interactAction.action.performed -= OnInteract;
        interactAction.action.canceled -= OnInteractCanceled;
        interactAction.action.Disable();
    }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            if (currentTarget == null)
            {
                CrosshairManager.Instance.TurnOn();
                currentTarget = hit.collider.GetComponent<IInteractable>();
                Debug.Log(currentTarget.GetInteractText());
            }
        }
        else
        {
            if (currentTarget != null)
                {
                    CrosshairManager.Instance.TurnOff();
                    currentTarget = null;
                }
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        if (currentTarget != null)
        {
            currentTarget.Interact(gameObject);
        }
    }

    private void OnInteractCanceled(InputAction.CallbackContext callback)
    {
        Debug.Log("Dejando de interactuar");
        if (activeInteractable is DoorController door)
        {
            door.StopInteraction();
        }

        activeInteractable = null;
    }
    private void OnGrab(InputAction.CallbackContext callback)
    {
        if (currentTarget is DoorController door && activeInteractable == null)
        {
            // Guardamos la puerta como el objeto activo
            activeInteractable = door;
            activeInteractable.Interact(gameObject); // Llama al Interact() de la puerta
        }
    }
    private void OnGrabCanceled(InputAction.CallbackContext callback)
    {
        // Si el objeto que estamos agarrando es una puerta, la soltamos
        if (activeInteractable is DoorController door)
        {
            door.StopInteraction();
        }

        
        activeInteractable = null;
    }
    private void OnDrop(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Dropeando Objeto");
        inventory.DropCurrentItem();

    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.coral;
        Gizmos.DrawRay(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)));
    }

}
