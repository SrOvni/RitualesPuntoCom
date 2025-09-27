using UnityEngine;

public class Tape : Item, IUsable
{
    public void Use(GameObject user)
    {
        Debug.Log("Usando");

        RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
        if (ritualManager != null)
        {
            ritualManager.MarkUsedItemStepComplete();
        }
    }
}
