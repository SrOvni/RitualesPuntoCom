using UnityEngine;

public interface IInteractable
{
    // Lo que aparece en pantalla cuando el jugador mira el objeto
    public string GetInteractText();

    // Qué pasa cuando el jugador interactúa
    public void Interact(GameObject interactor);
}
