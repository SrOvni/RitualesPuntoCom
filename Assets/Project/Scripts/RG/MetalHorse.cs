using System;
using UnityEngine;
using System.Collections;

public class MetalHorse : Interactable
{
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; } = delegate { };
    private void Start()
    {
        OnInteract += TryInsertCoint;
        Debug.Log("Hola");
    }

    private void TryInsertCoint(RaycastHit hit, InteractionHandler handler)
    {
        if(!CanInteract) return;
        Debug.Log("Hola 1");
        if (handler.TryGetComponent(out Inventory inventory))
        {
            Debug.Log("Hola 2");
            if (inventory.CurrentItem.TryGetComponent(out Interactable interactable))
            {
                if (interactable.Type == requiredItemType)
                {
                    Debug.Log("Moneda colocada");
                    StartCoroutine(CoinAnimation(interactable.gameObject));
                    StartCoroutine(HorseDreamManager.Instance.HorseDream());
                }
            }
            else
            {
                Debug.Log("Item incorrecto");
            }
        }
    }

    private IEnumerator CoinAnimation(GameObject _)
    {
        Debug.Log("Coin animation");
        yield return null;
    }

    [SerializeField] ItemType requiredItemType = ItemType.Coin;
    ItemType type;

    public override ItemType Type => type;

    public override bool CanInteract { get; set; } = false;

    public override string GetInteractText()
    {
        return "Insert coin";
    }

    public override void Interact(GameObject interactor)
    {
        Debug.Log("Hola");
    }

    
}
