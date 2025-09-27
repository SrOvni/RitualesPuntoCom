using System;
using UnityEngine;
using System.Linq;

public class MetalHorse : Interactable
{
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    private void Start() {
        OnInteract += TryInsertCoint;
    }

    private void TryInsertCoint(RaycastHit hit, InteractionHandler handler)
    {
        if (handler.TryGetComponent(out Inventory inventory))
        {
            if (inventory.InvetoryList.Any(i => i != null && i.GetComponent<Interactable>().Type == requiredItemType))
            {
                Debug.Log("Moneda colocada");
                StartCoroutine(GameManager.Instance.HorseDream());
            }
        }
    }

    [SerializeField] ItemType requiredItemType = ItemType.Coin;
    ItemType type;

    public override ItemType Type => type;

    public override string GetInteractText()
    {
        return "MetalHorse";
    }

    public override void Interact(GameObject interactor)
    {
        //
    }

    
}
