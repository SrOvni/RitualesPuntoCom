using UnityEngine;

public class Bed : MonoBehaviour,IInteractable
{
    public string GetInteractText()
    {
        return "A mimir";
    }
    public void Interact(GameObject interactor)
    {

    }
}
