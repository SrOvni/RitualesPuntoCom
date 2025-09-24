using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    Rigidbody Rb;
    private void Awake() {
        Rb.linearDamping = -10;
    }
    [SerializeField] private string interactText = "Recoger";

    public string GetInteractText()
    {
        return interactText;
    }

    public void Interact(GameObject interactor)
    {
        // Buscamos el componente Inventory en el jugador
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(gameObject);

            // Opcional: desactivar colisiones y f�sicas para que no estorbe
            Collider col = GetComponent<Collider>();
            if (col != null) col.enabled = false;

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = true;
        }
        else
        {
            Debug.LogWarning("El interactor no tiene un componente Inventory");
        }
    }
}
