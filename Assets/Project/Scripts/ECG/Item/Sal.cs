using UnityEngine;

public class Sal : Item, IUsable
{
    [SerializeField] private GameObject saltCirclePrefab;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float spawnOffset = 0.05f;
    public void Use(GameObject user)
    {
        Inventory inventory = user.GetComponent<Inventory>();  

        if (saltCirclePrefab != null)
        {
            
            Ray ray = new Ray(user.transform.position, Vector3.down);
            RaycastHit hit;

            
            if (Physics.Raycast(ray, out hit, 10f, groundLayer))
            {

                Vector3 spawnPosition = hit.point + new Vector3(0, spawnOffset, 0);
                Instantiate(saltCirclePrefab, spawnPosition, Quaternion.identity);
            }
            else
            {

                Vector3 spawnPosition = user.transform.position + new Vector3(0, -1f + spawnOffset, 0);
                Instantiate(saltCirclePrefab, spawnPosition, Quaternion.identity);
            }

            inventory.DeleteItem(this.gameObject);

        }

        // Ahora, informar al RitualManager.
        RitualManager ritualManager = FindAnyObjectByType<RitualManager>();
        if (ritualManager != null)
        {
            ritualManager.MarkUsedItemStepComplete();
        }
    }
}
