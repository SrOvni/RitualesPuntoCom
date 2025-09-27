using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private PlacementPoint[] ritualPoints;
    [SerializeField] private int totalRitualSteps = 1; // Solo los items que se Usan

    private int completedSteps = 0;
    private bool isRitualCompleted = false;

    // Se llama cuando un PlacementPoint recibe el ítem correcto
    public void MarkPlacementStepComplete()
    {
        CheckForCompletion();
    }

    // Se llama cuando un ítem "usable" se usa correctamente
    public void MarkUsedItemStepComplete()
    {
        completedSteps++;
        Debug.Log($"Paso del ritual completado. Total de pasos: {completedSteps} de {totalRitualSteps}");
        CheckForCompletion();
    }
    private bool AllPointsFilledAndValid()
    {
        foreach (var point in ritualPoints)
        {
            
            if (!point.IsFilled || !point.IsValidPlacement)
            {
                return false;
            }
        }
        // Si el se completa entonces, todos los espacios son validos y estan ocupados
        return true;
    }
    private void CheckForCompletion()
    {
 
        if (AllPointsFilledAndValid() && (completedSteps == totalRitualSteps) && !isRitualCompleted)
        {
            isRitualCompleted = true;
            OnRitualComplete();
        }
    }


    private void OnRitualComplete()
    {
        Debug.Log("¡El ritual ha comenzado!");
        
        this.enabled = false;
    }
}
