using System;
using UnityEngine;
using System.Collections;

public class MetalHorse : Interactable
{
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; } = delegate { };
    [SerializeField] bool canInteract = false;
    private void Awake()
    {
        OnInteract += TryInsertCoint;
    }
    [Header("Movimiento vertical")]
    [SerializeField] private float verticalAmplitude = 0.5f; // Altura máxima (arriba/abajo)
    [SerializeField] private float verticalDuration = 2f;    // Tiempo de subida o bajada

    [Header("Balanceo lateral (opcional)")]
    [SerializeField] private float swayAngle = 10f;          // Grados de inclinación
    [SerializeField] private float swayDuration = 3f;        // Tiempo de ida/vuelta del balanceo

    private void DoCarousel()
    {
        /*
        transform
            .DOMoveY(transform.position.y + verticalAmplitude, verticalDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        // Balanceo lateral suave (rotación alrededor del eje Z, como un ligero mecer)
        transform
            .DORotate(new Vector3(0f, 0f, swayAngle), swayDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
            */
        
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
