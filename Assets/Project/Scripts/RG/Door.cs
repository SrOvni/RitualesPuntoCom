using System;
using UnityEngine;
public class Door : Interactable
{
    [SerializeField] GameObject doorKnob;
    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; }

    void Awake()
    {
        OnInteract += SetDoorKnob;
        OnInteractionStop += HideDoorKnob;
    }

    private void SetDoorKnob(RaycastHit hit, InteractionHandler interactionHandler)
    {
        Debug.Log("Working raycast");
        doorKnob.transform.position = new Vector3(doorKnob.transform.position.x, hit.point.y, doorKnob.transform.position.z);
        doorKnob.SetActive(true);
        // doorKnob.transform.SetParent(interactionHandler.gameObject.transform);

    }
    private void HideDoorKnob()
    {
        doorKnob.SetActive(false);
        // doorKnob.transform.SetParent(null);
    }

    public override string GetInteractText()
    {
        return "Door";
    }

    public override void Interact(GameObject interactor)
    {

    }
}

