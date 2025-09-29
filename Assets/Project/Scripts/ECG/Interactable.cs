using System;
using UnityEditor;
using UnityEngine;

public abstract class Interactable: MonoBehaviour, IInteractable
{
    public abstract bool CanInteract { get; set; }
    public abstract Action<RaycastHit, InteractionHandler> OnInteract { get; set; }
    public abstract Action OnInteractionStop { get; set; }
    public abstract Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; }
    public abstract ItemType Type { get; }
    public abstract string GetInteractText();

    public void Interact(RaycastHit hit, InteractionHandler interactor)
    {
        OnInteract?.Invoke(hit, interactor);
    }
    public void StopInteraction()
    {
        OnInteractionStop?.Invoke();
    }

    public abstract void Interact(GameObject interactor);
}
