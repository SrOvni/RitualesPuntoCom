using UnityEngine;
using UnityEngine.InputSystem;

public class DoorGrab : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask doorLayer;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference grabAction;       // Button

    [Header("Grab Configuration")]
    [SerializeField] private float speedMultiplier = 60000;



    private Transform selectedDoor;
    private Transform pivotDoor;
    private GameObject dragPointGameobject;
    private int leftDoor = 0;

    void OnEnable()
    {
        grabAction.action.Enable();
    }

    void OnDisable()
    {
        grabAction.action.Disable();
    }

    void Update()
    {
        // --- Detectar inicio de agarre ---
        if (grabAction.action.WasPressedThisFrame())
        {
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 3f, doorLayer))
            {
                selectedDoor = hit.collider.transform;
                pivotDoor = selectedDoor.parent; // usamos PivotDoor

                // Crear drag point solo una vez
                if (dragPointGameobject == null)
                {
                    dragPointGameobject = new GameObject("Ray door");
                    dragPointGameobject.transform.parent = selectedDoor;
                }

                Debug.Log($"Puerta seleccionada: {selectedDoor.name}, Pivot: {pivotDoor.name}");
            }
        }

        // --- Mientras agarro la puerta ---
        if (selectedDoor != null)
        {
            HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
            if (joint == null) return;

            JointMotor motor = joint.motor;

            // Posición del drag point (en base al pivotDoor como referencia)
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            dragPointGameobject.transform.position = ray.GetPoint(Vector3.Distance(selectedDoor.position, transform.position));
            dragPointGameobject.transform.rotation = selectedDoor.rotation;

            // Debug para ver posiciones
            Debug.Log(
                $"PivotDoor Pos: {pivotDoor.localPosition}, " +
                $"Door Pos: {selectedDoor.localPosition}, " +
                $"DragPoint: {dragPointGameobject.transform.localPosition}"
            );

            float delta = Mathf.Pow(Vector3.Distance(dragPointGameobject.transform.position, pivotDoor.position), 2);

            // Determinar izquierda/derecha basado en pivot
            leftDoor = (selectedDoor.localPosition.x > pivotDoor.localPosition.x) ? 1 : -1;

            // Aplicar velocidad al motor según orientación
            if (Mathf.Abs(pivotDoor.forward.z) > 0.5f)
            {
                motor.targetVelocity = (dragPointGameobject.transform.position.x > pivotDoor.position.x)
                    ? delta * -speedMultiplier * Time.deltaTime * leftDoor
                    : delta * speedMultiplier * Time.deltaTime * leftDoor;
            }
            else
            {
                motor.targetVelocity = (dragPointGameobject.transform.position.z > pivotDoor.position.z)
                    ? delta * -speedMultiplier * Time.deltaTime * leftDoor
                    : delta * speedMultiplier * Time.deltaTime * leftDoor;
            }

            joint.motor = motor;
        }

        // --- Soltar puerta ---
        if (grabAction.action.WasReleasedThisFrame() && selectedDoor != null)
        {
            HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
            if (joint != null)
            {
                JointMotor motor = joint.motor;
                motor.targetVelocity = 0;
                joint.motor = motor;
            }

            Debug.Log("Puerta soltada");

            if (dragPointGameobject != null) Destroy(dragPointGameobject);
            dragPointGameobject = null;
            selectedDoor = null;
            pivotDoor = null;
        }
    }
}

