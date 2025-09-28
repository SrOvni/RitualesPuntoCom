using DG.Tweening;
using UnityEngine;

public class InventoryAnimation : MonoBehaviour
{
    [Tooltip("La escala a la que se animar� el �tem cuando est� seleccionado.")]
    [SerializeField] private float highlightScaleFactor = 1.1f;
    [Tooltip("La duraci�n de la animaci�n.")]
    [SerializeField] private float animationDuration = 0.2f;

    private Vector3 originalScale;
    private Tween currentTween; // Para evitar conflictos en la animaci�n

    private void Awake()
    {
        // Almacena la escala original del objeto, sin importar cu�l sea
        originalScale = transform.localScale;
    }

    public void AnimateSelection()
    {
        // Detiene cualquier animaci�n en curso para evitar deformaciones
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill(true);
        }

        // Calcula la escala de destino multiplicando la escala original por el factor
        Vector3 targetScale = originalScale * highlightScaleFactor;

        // Anima a la nueva escala
        currentTween = transform.DOScale(targetScale, animationDuration).SetEase(Ease.OutBack);
    }

    public void AnimateDeselection()
    {
        // Detiene cualquier animaci�n en curso
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill(true);
        }

        // Anima de vuelta a la escala original guardada
        currentTween = transform.DOScale(originalScale, 0);
    }

    // Opcional: Para mayor seguridad, aseg�rate de que el objeto vuelva a la escala original
    // si el script se desactiva.
    private void OnDisable()
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
        transform.localScale = originalScale;
    }

    // Opcional: Destruir el tween si el objeto es destruido
    private void OnDestroy()
    {
        if (currentTween != null)
        {
            currentTween.Kill();
        }
    }
}
