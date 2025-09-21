using UnityEngine;
using UnityEngine.InputSystem;

public class DoorGrab : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask doorLayer;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference grabAction;       // Button
    [SerializeField] private InputActionReference lookPositionAction; // Vector2 (mouse or touch)

    private Transform selectedDoor;
    private GameObject dragPointGameobject;
    private int leftDoor = 0;

    void OnEnable()
    {
        grabAction.action.Enable();
        lookPositionAction.action.Enable();
    }

    void OnDisable()
    {
        grabAction.action.Disable();
        lookPositionAction.action.Disable();
    }

    void Update()
    {
        RaycastHit hit;

        // Detectar puerta al presionar
        if (grabAction.action.WasPressedThisFrame())
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit,1000, doorLayer))
            {
                selectedDoor = hit.collider.transform;
                Debug.Log("Puerta seleccionada");
            }
            Debug.Log("grabAction action activada");
        }

        if (selectedDoor != null)
        {
            HingeJoint joint = selectedDoor.GetComponent<HingeJoint>();
            JointMotor motor = joint.motor;

            // Crear drag point si no existe
            if (dragPointGameobject == null)
            {
                dragPointGameobject = new GameObject("Ray door");
                dragPointGameobject.transform.parent = selectedDoor;
            }

            // Obtener posición de cursor/pantalla desde nuevo input
            Vector2 screenPos = lookPositionAction.action.ReadValue<Vector2>();
            Ray ray = cam.ScreenPointToRay(screenPos);

            dragPointGameobject.transform.position = ray.GetPoint(Vector3.Distance(selectedDoor.position, transform.position));
            dragPointGameobject.transform.rotation = selectedDoor.rotation;

            float delta = Mathf.Pow(Vector3.Distance(dragPointGameobject.transform.position, selectedDoor.position), 3);

            // Determinar si puerta es izquierda o derecha
            if (selectedDoor.GetComponent<MeshRenderer>().localBounds.center.x > selectedDoor.localPosition.x)
                leftDoor = 1;
            else
                leftDoor = -1;

            // Aplicar velocidad al motor
            float speedMultiplier = 60000;
            if (Mathf.Abs(selectedDoor.parent.forward.z) > 0.5f)
            {
                motor.targetVelocity = (dragPointGameobject.transform.position.x > selectedDoor.position.x)
                    ? delta * -speedMultiplier * Time.deltaTime * leftDoor
                    : delta * speedMultiplier * Time.deltaTime * leftDoor;
            }
            else
            {
                motor.targetVelocity = (dragPointGameobject.transform.position.z > selectedDoor.position.z)
                    ? delta * -speedMultiplier * Time.deltaTime * leftDoor
                    : delta * speedMultiplier * Time.deltaTime * leftDoor;
            }

            joint.motor = motor;

            // Soltar puerta
            if (grabAction.action.WasReleasedThisFrame())
            {
                selectedDoor = null;
                motor.targetVelocity = 0;
                joint.motor = motor;
                Destroy(dragPointGameobject);
            }
        }
    }
}

