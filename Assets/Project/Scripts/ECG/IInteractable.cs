using UnityEngine;

public interface IInteractable
{
    // Lo que aparece en pantalla cuando el jugador mira el objeto
    public string GetInteractText();

    // Qu� pasa cuando el jugador interact�a
    public void Interact(GameObject interactor);
    
}
