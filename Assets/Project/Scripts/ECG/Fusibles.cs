using UnityEngine;

public class Fusibles : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Fusibles";
    public string GetInteractText()
    {
        return interactText;
    }
    public void Interact(GameObject interactor)
    {
        LightManager.Instance.TurnOnLights();
    }
}
