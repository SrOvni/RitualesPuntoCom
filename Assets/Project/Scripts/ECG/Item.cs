using UnityEngine;
public enum ItemType
{
    None,
    Skull,
    Candle,
    Book,
    Key,
    Coin,
    Computer
}
public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Pickup";
    [SerializeField] private ItemType itemType = ItemType.None;
    Rigidbody Rb;
    // [SerializeField] private string interactText = "Recoger";

    public ItemType Type => itemType;
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

            //desactivar colisiones y f�sicas para que no estorbe
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
