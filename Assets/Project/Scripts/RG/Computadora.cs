using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class Computadora : Interactable
{
    [SerializeField] private MeshRenderer screen;
    [SerializeField] private Canvas UICanvas;
    Color on = Color.white;
    Color off = Color.black;
    Material screenMaterial;

    public override Action<RaycastHit, InteractionHandler> OnInteract { get; set; } = delegate { };
    public override Action OnInteractionStop { get; set; } = delegate { };
    public override Action<RaycastHit, InteractionHandler> OnInteractionPerformed { get; set; } = delegate { };
    [SerializeField] ItemType type;
    public override ItemType Type => type;

    public override bool CanInteract { get; set; } = true;

    [SerializeField] InputReader inputReader;
    [SerializeField] LayerMask computerScreenLayerMask;

    bool updateCursor = false;
    public Action OnComputerTurnOn = delegate { };

    void Awake()
    {
        OnInteractionPerformed += GetScreenPosition;
        OnInteract += Interact;
        // screenMaterial = screen.material;
        // screenMaterial.color = off;
        UICanvas.gameObject.SetActive(false);

    }
    public void TurnOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // screenMaterial.color = on;
        // screen.gameObject.SetActive(true);
        // computerCursor.gameObject.SetActive(true);
        updateCursor = true;
        OnComputerTurnOn?.Invoke();
        UICanvas.gameObject.SetActive(true);

    }
    void GetScreenPosition(RaycastHit hit, InteractionHandler handler)
    {
        Debug.Log("Screen On");
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out RaycastHit interlaHit, 1, computerScreenLayerMask))
        {
            // computerCursor.position = interlaHit.point;
        }
    }

    public void TurnOff()
    {
        // screenMaterial.color = off;
        // screen.gameObject.SetActive(false);
        UICanvas.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        // computerCursor.gameObject.SetActive(false);
    }

    public override string GetInteractText()
    {
        return "Computadora";
    }

    public override void Interact(GameObject interactor)
    {
        //
    }
    public new void Interact(RaycastHit hit, InteractionHandler interactor)
    {
        GetComponent<Collider>().enabled = false;
        CameraManager.Instance.PrioritizeComputerCamera();

        StartCoroutine(TurnOnScreenWhenBlend());
        inputReader.DisableMovementInputs();
        // Cursor.lockState = CursorLockMode.Confined;
        // Cursor.visible = true;
    }
    public void TurnOffComputer()
    {
        CameraManager.Instance.PrioritizePlayerCamera();
        TurnOff();
        inputReader.EnableMovmentInput();   
        enabled = false;
    }

    public IEnumerator TurnOnScreenWhenBlend()
    {
        yield return new WaitForSeconds(CameraManager.Instance.Brain.DefaultBlend.Time);
        TurnOn();
    }
}
