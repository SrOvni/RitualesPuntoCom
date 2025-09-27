using System;
using UnityEngine;

public class Coin : Interactable
{
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; } = delegate { };

    public override ItemType Type => type;

    [SerializeField] ItemType type;

    public override string GetInteractText()
    {
        return "Coin";
    }

    public override void Interact(GameObject interactor)
    {
        //
    }
    void Awake()
    {
        OnInteract += Pickup;
    }

    private void Pickup(RaycastHit _, InteractionHandler __)
    {
        Inventory inventory = __.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.AddItem(gameObject);
        }
        if (TryGetComponent(out Collider col))
        {
            col.enabled = false;
        }
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = true;
        }
    }
}
