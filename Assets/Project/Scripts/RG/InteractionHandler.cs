using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class InteractionHandler : MonoBehaviour
{

    [SerializeField] InputActionReference interactionInputActionReference;
    Ray ray;
    [SerializeField] float interactionDistance = 2;
    [SerializeField] GameObject righHand;
    [SerializeField] LayerMask layerMask;
    RaycastHit hit;
    [SerializeField] private Camera cam;
    Interactable currentInteractable;

    private void Awake()
    {

        interactionInputActionReference.action.Enable();
        interactionInputActionReference.action.started += OnInteract;

    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
            if (Physics.Raycast(ray, out hit, interactionDistance, layerMask))
            {
                if (currentInteractable == null)
                {
                    currentInteractable = hit.collider.GetComponent<Interactable>();
                    currentInteractable?.OnInteract?.Invoke(hit, this);
                }
                else
                {
                    currentInteractable.OnInteractionPerformed?.Invoke(hit, this);
                }
            }
            else
            {
                if (currentInteractable != null)
                {
                    currentInteractable = null;
                }
            }
        }
    }
    void OnDisable()
    {
        interactionInputActionReference.action.Disable();
        interactionInputActionReference.action.started -= OnInteract;
    }
}
