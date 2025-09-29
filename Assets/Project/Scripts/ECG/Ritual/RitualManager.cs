using System.Collections.Generic;
using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private List<PlacementPoint> ritualPoints = new List<PlacementPoint>();
    [SerializeField] private int totalRitualSteps = 1; // Solo los items que se Usan

    private int completedSteps = 0;
    [SerializeField] private bool isRitualCompleted = false;

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

    public void AddRitualPoints(GameObject ritual)
    {
        // 1. Encuentra todos los componentes PlacementPoint en el objeto y sus hijos
        PlacementPoint[] points = ritual.GetComponentsInChildren<PlacementPoint>();

        // 2. Itera sobre los puntos encontrados y agrégalos a tu lista principal
        foreach (PlacementPoint point in points)
        {
            ritualPoints.Add(point);
            //Debug.Log($"Punto de colocación '{point.name}' agregado al ritual.");
        }
    }

    private void OnRitualComplete()
    {
        Debug.Log("¡El ritual ha comenzado!");
        
        this.enabled = false;

        foreach (var point in ritualPoints)
        {
            // Assuming PlacementPoint has a method to get the placed item
            GameObject placedItem = point.GetPlacedItem();

            if (placedItem != null)
            {
                Candle candle = placedItem.GetComponent<Candle>();
                if (candle != null)
                {
                    candle.LightCandle();
                }
            }
        }

        LightManager.Instance.TurnOffLights();
    }
}
