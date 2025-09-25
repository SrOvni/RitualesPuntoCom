using UnityEngine;

public class RitualManager : MonoBehaviour
{
    [SerializeField] private PlacementPoint[] ritualPoints;

    void Update()
    {

        if (AllPointsFilledAndValid())
        {
            Debug.Log("¡El ritual ha comenzado!");
           
            
        }
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
}
