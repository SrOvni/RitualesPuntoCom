using System;
using UnityEngine;

public class Microwave : Interactable
{
    public override bool CanInteract { get; set; } = true;
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; }
    public override Action OnInteractionStop  { get; set; }
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed  { get; set; }

    void Awake()
    {
        OnInteract += PlayerAre;
    }

    private void PlayerAre(RaycastHit hit, InteractionHandler handler)
    {
        DayOneManager.Instance.PlayerAte = true;
    }

    public override ItemType Type => throw new NotImplementedException();

    public override string GetInteractText()
    {
        return "Microndas";
    }

    public override void Interact(GameObject interactor)
    {
        throw new NotImplementedException();
    }
}
