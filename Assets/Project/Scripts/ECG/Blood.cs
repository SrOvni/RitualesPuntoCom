using DG.Tweening;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [Tooltip("La velocidad a la que se mueve el plano.")]
    [SerializeField] private float speed = 1.0f;
    [Tooltip("La distancia que se mover� el plano.")]
    [SerializeField] private float travelDistance = 10.0f;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        // 1. Calcula la posici�n final del movimiento
        Vector3 endPosition = transform.position + new Vector3(0, travelDistance, 0);

        // 2. Inicia la animaci�n de movimiento
        transform.DOMove(endPosition, travelDistance / speed)
            .SetEase(Ease.Linear) // Mantiene la velocidad constante
            .SetLoops(-1, LoopType.Restart); // Repite la animaci�n indefinidamente
    }
}
