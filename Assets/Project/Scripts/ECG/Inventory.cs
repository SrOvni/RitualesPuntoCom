using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [Header("Inventory Config")]
    [SerializeField] Transform itemPosition;
    [SerializeField] List<GameObject> inventoryList = new List<GameObject>();
    public List<GameObject> InvetoryList => inventoryList;

    Vector3 scale;

    private int currentIndex = -1;
    [SerializeField] private GameObject currentItem;
    public GameObject CurrentItem => currentItem;

    private void Start()
    {
        GameObject emptyholder = new GameObject("Empty Holder");
        inventoryList.Add(emptyholder);
        // Inicializar el índice al primer elemento (el emptyholder)
        currentIndex = 0;
        currentItem = emptyholder;
    }

    public GameObject GetCurrentItem()
    {
        return currentItem;
    }
    public void AddItem(GameObject item)
    {
        // A�adir item a la lista
        inventoryList.Add(item);

        // Desactivar en el mundo
        item.SetActive(false);

        //desactivar colliders
        Collider[] itemColliders = item.GetComponents<Collider>();
        foreach (Collider col in itemColliders)
        {
            col.enabled = false;
        }

        // Si es el primer item, seleccionarlo

        currentIndex = inventoryList.Count - 1;


        ShowItemAtIndex(currentIndex);
        ParentItem(currentIndex);
    }

    private void ParentItem(int currentIndex)
    {
        scale = currentItem.transform.lossyScale;
        //Debug.Log(scale);
        currentItem.transform.SetParent(itemPosition);
        currentItem.transform.localPosition = Vector3.zero;
        currentItem.transform.localRotation = Quaternion.identity;
    }

    public void DropCurrentItem()
    {
        // Revisa si hay un objeto seleccionado y si no es el 'Empty Holder'
        if (currentItem == null || currentIndex <= 0) return;

        // Guarda el objeto actual para soltarlo
        GameObject itemToDrop = currentItem;

        currentItem = null;

        // Elimina el objeto de la lista
        inventoryList.RemoveAt(currentIndex);

        // Desparenta y activa el objeto en el mundo
        itemToDrop.transform.SetParent(null);

        //aitemToDrop.transform.localScale = scale; // Usa la escala que guardaste al recogerlo
        itemToDrop.SetActive(true);
        itemToDrop.transform.position = itemPosition.position;
        itemToDrop.transform.rotation = itemPosition.rotation;

        // Reactiva las colisiones y la física
        Collider[] colliders = itemToDrop.GetComponents<Collider>();

        foreach (Collider col in colliders)
        {
            col.enabled = true;
        }

        Rigidbody rb = itemToDrop.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.linearVelocity = Vector3.zero;
        }

        // AHORA ajusta el índice para seleccionar el siguiente objeto
        if (inventoryList.Count > 1)
        {
            // El índice se ajusta automáticamente si no es el último elemento
            // Si el índice es mayor o igual al nuevo tamaño de la lista,
            // lo movemos al último elemento de la lista para evitar un error.
            if (currentIndex >= inventoryList.Count)
            {
                currentIndex = inventoryList.Count - 1;
            }

            ShowItemAtIndex(currentIndex);
        }
        else
        {
            // Si solo queda el 'Empty Holder', selecciona ese
            currentIndex = 0;
            currentItem = inventoryList[0];
            ShowItemAtIndex(currentIndex);
            
        }
    }

    public void PlaceCurrentItem(PlacementPoint placementPoint)
    {
        // Revisa si hay un ítem para colocar y si no es el empty holder.
        if (currentItem == null || currentIndex <= 0) return;

        // Obtener el componente Item del objeto actual
        Item itemComponent = currentItem.GetComponent<Item>();
        if (itemComponent == null) return;

        // Obtener el tipo de ítem antes de colocarlo
        ItemType placedItemType = itemComponent.Type;

        // Eliminar el objeto del inventario.
        GameObject itemToPlace = currentItem;

        inventoryList.RemoveAt(currentIndex);

        currentItem = null;

        // Ajustar el índice y mostrar el siguiente ítem
        if (inventoryList.Count > 1)
        {
            currentIndex = (currentIndex > inventoryList.Count - 1) ? inventoryList.Count - 1 : currentIndex;
            ShowItemAtIndex(currentIndex);
        }
        else
        {
            currentIndex = 0;
            currentItem = inventoryList[0];
            ShowItemAtIndex(currentIndex);
        }

        // Llamar al método del punto de colocación, pasándole el ítem y su tipo.
        // La lógica de validación se manejará dentro del PlacementPoint.
        placementPoint.MarkAsFilled(itemToPlace, placedItemType);


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
        currentItem.SetActive(true);
    }
}
