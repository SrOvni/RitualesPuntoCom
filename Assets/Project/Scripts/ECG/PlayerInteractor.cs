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
    private IInteractable currentTarget;

    void OnEnable()
    {

        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
        dropAction.action.Enable();
        dropAction.action.performed += OnDrop;
    }

    void OnDisable()
    {
        
        dropAction.action.performed += OnDrop;
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
        dropAction.action.Disable();
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
            currentTarget = hit.collider.GetComponent<IInteractable>();

            if (currentTarget != null)
            {
                Debug.Log(currentTarget.GetInteractText());
                // Aquí puedes mostrar un UI con ese texto
            }
        }
        else
        {
            currentTarget = null;
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        

        if (currentTarget != null)
        {
            currentTarget.Interact(gameObject);
        }
    }

    private void OnDrop(InputAction.CallbackContext ctx)
    {
        Debug.Log("Dropeando Objeto");
        inventory.DropCurrentItem();

    }

}
