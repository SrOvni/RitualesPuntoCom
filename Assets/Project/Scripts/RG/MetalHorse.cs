using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MetalHorse : Interactable
{
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; } = delegate { };
    [SerializeField] bool canInteract = false;
    public GameObject toyHorse;
    private void Awake()
    {
        OnInteract += TryInsertCoint;
    }
    [Header("Movimiento circular")]
    [SerializeField] private float radius = 2f;        // radio del círculo
    [SerializeField] private float circleDuration = 6f; // tiempo para dar una vuelta

    [Header("Movimiento vertical")]
    [SerializeField] private float verticalAmplitude = 0.5f; // altura de subida/bajada
    [SerializeField] private float verticalDuration = 2f;
    private Vector3 centerPos;

    public void DoCarousel()
    {
        toyHorse.transform
            .DOMoveY(centerPos.y + verticalAmplitude, verticalDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void TryInsertCoint(RaycastHit hit, InteractionHandler handler)
    {
        if (!CanInteract) return;
        if (handler.TryGetComponent(out Inventory inventory))
        {
            if (inventory.CurrentItem.TryGetComponent(out Interactable interactable))
            {
                if (interactable.Type == requiredItemType)
                {
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

    public override bool CanInteract { get => canInteract; set => canInteract = value; }

    public override string GetInteractText()
    {
        return "Insert coin";
    }

    public override void Interact(GameObject interactor)
    {
        Debug.Log("Hola");
    }


}
