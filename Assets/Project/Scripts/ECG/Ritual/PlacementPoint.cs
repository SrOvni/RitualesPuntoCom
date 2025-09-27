using RG.Systems;
using UnityEngine;

public class PlacementPoint : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Colocar objeto aqu�";

    [Header("Ritual Config")]
    [SerializeField] private ItemType requiredItemType;
    [SerializeField] private bool isPartOfRitual = true;
    public bool IsFilled { get; private set; } = false;
    public bool IsValidPlacement { get; private set; } = false;

    public string GetInteractText()
    {
        return interactText;
    }

    

    public void Interact(GameObject interactor)
    {
        // Si el punto está lleno, se asume que el jugador quiere recoger el ítem.
        if (IsFilled)
        {
            PickUpItem(interactor);
        }
        // Si el punto está vacío, se asume que el jugador quiere colocar un ítem.
        else
        {
            Inventory inventory = interactor.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.PlaceCurrentItem(this);
            }
        }
    }

    private void PickUpItem(GameObject interactor)
    {
        // Revisa si hay un item para recoger
        if (!IsFilled) return;

        // Obtiene el item de la jerarquía del PlacementPoint
        // Asumiendo que el item es un hijo directo
        GameObject itemToPickUp = transform.GetChild(0).gameObject;
        if (itemToPickUp == null) return;

        // Notifica al inventario
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(itemToPickUp);
            // Resetea el estado del PlacementPoint
            UnmarkAsFilled();
        }
    }

    public void MarkAsFilled(GameObject item, ItemType placedItemType)
    {
        IsFilled = true;

        // Compara el tipo de ítem colocado con el tipo requerido
        IsValidPlacement = (placedItemType == requiredItemType);



        item.transform.SetParent(transform);
        item.transform.position = transform.position;
        item.transform.rotation = transform.rotation;

        Collider[] itemColliders = item.GetComponents<Collider>();
        foreach (Collider col in itemColliders)
        {
            col.enabled = false;
        }

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.isKinematic = true;
            
        }

        if (IsValidPlacement && isPartOfRitual)
        {
            RitualManager ritualManager = FindAnyObjectByType<RitualManager>(); 
            if (ritualManager != null)
            {
                ritualManager.MarkPlacementStepComplete();
            }
        }
        else
        {
            Debug.Log($"Colocación no válida. Se colocó un {placedItemType} en un punto que requería un {requiredItemType}.");
        }
    }
    public void UnmarkAsFilled()
    {
        IsFilled = false;
        IsValidPlacement = false;
        
        Debug.Log("Punto de colocación ha sido reseteado.");
    }
}
