using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private InputActionReference interactAction;

    private IInteractable currentTarget;

    void OnEnable()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    void OnDisable()
    {
        interactAction.action.performed -= OnInteract;
        interactAction.action.Disable();
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
        Debug.Log("Interactuando");

        if (currentTarget != null)
        {
            currentTarget.Interact(gameObject);
        }
    }
}
