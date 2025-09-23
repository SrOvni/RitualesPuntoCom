using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] Transform itemPosition;
    [SerializeField] List<GameObject> inventoryList = new List<GameObject>();

    private int currentIndex = -1;
    private GameObject currentItem;

    private void Start()
    {
        GameObject emptyholder = new GameObject("Empty Holder");
        inventoryList.Add(emptyholder);
    }
    public void AddItem(GameObject item)
    {
        // Añadir item a la lista
        inventoryList.Add(item);

        // Desactivar en el mundo
        item.SetActive(false);

        // Si es el primer item, seleccionarlo
      
            currentIndex = inventoryList.Count - 1;
            
            ShowItemAtIndex(currentIndex);
    }

    void Update()
    {
        // --- Detectar scroll con la rueda ---
        float scroll = Mouse.current.scroll.ReadValue().y;

        if (scroll > 0) // scroll hacia arriba
        {
            SelectPreviousItem();
        }
        else if (scroll < 0) // scroll hacia abajo
        {
            SelectNextItem();
        }
    }
    private void SelectNextItem()
    {
        if (inventoryList.Count == 0) return;

        currentIndex = (currentIndex + 1) % inventoryList.Count;
        ShowItemAtIndex(currentIndex);
    }

    private void SelectPreviousItem()
    {
        if (inventoryList.Count == 0) return;

        currentIndex--;
        if (currentIndex < 0) currentIndex = inventoryList.Count - 1;
        ShowItemAtIndex(currentIndex);
    }

    private void ShowItemAtIndex(int index)
    {
        if (index < 0 || index >= inventoryList.Count) return;

        // Ocultar item actual
        if (currentItem != null)
        {
            currentItem.SetActive(false);
        }

        // Mostrar nuevo item
        currentItem = inventoryList[index];
        currentItem.transform.SetParent(itemPosition);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
        currentItem.SetActive(true);
    }
}
