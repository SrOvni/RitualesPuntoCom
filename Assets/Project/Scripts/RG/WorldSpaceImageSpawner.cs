using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceImageSpawner : MonoBehaviour
{
    [Header("Imagen a mostrar")]
    public Sprite imageSprite;       // Asigna el sprite desde el Inspector
    public Vector3 offset = new Vector3(0, .2f, 0); // 20 cm arriba aprox.

    Canvas spawnedCanvas;

    public void SpawnWorldSpaceCanvas()
    {
        // 1️⃣ Crear GameObject Canvas
        GameObject canvasGO = new GameObject("WorldSpaceCanvas");
        spawnedCanvas = canvasGO.AddComponent<Canvas>();
        spawnedCanvas.renderMode = RenderMode.WorldSpace;

        // 2️⃣ Ajustar RectTransform
        RectTransform canvasRect = canvasGO.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(1f, 1f); // tamaño en metros
        canvasGO.transform.SetParent(transform);    // que sea hijo del objeto
        canvasGO.transform.localPosition = offset;  // posición relativa (cm arriba)
        canvasGO.transform.localRotation = Quaternion.identity;

        // 3️⃣ Añadir un CanvasScaler para buena resolución
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 100;

        // 4️⃣ Añadir un componente GraphicRaycaster si necesitas interacción
        canvasGO.AddComponent<GraphicRaycaster>();

        // 5️⃣ Crear la Image
        GameObject imageGO = new GameObject("Image");
        imageGO.transform.SetParent(canvasGO.transform, false);

        Image img = imageGO.AddComponent<Image>();
        img.sprite = imageSprite;
        img.preserveAspect = true;

        RectTransform imgRect = imageGO.GetComponent<RectTransform>();
        imgRect.sizeDelta = new Vector2(0.5f, 0.5f); // medio metro de ancho/alto aprox.

        // Opcional: que siempre mire a la cámara
        canvasGO.AddComponent<LookAtCamera>();
    }
}
