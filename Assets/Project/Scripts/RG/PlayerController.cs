using System.Runtime.CompilerServices;
using RG.Systems;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlayerContext
{
    Rigidbody rb;
    [SerializeField] InputReader input;
    [SerializeField] float rotationSpeed = 400;
    [SerializeField] float moveSpeed = 10;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        input.EnablePlayerInputActions();
    }

    void FixedUpdate()
    {

        Vector3 moveDirection = transform.forward * input.Direction.y * moveSpeed * Time.fixedDeltaTime;
        float rotateAmount = input.Direction.x *  rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotateAmount, 0f);

        // Mover el Rigidbody de forma continua
        rb.MovePosition(rb.position + moveDirection);

        // Rotar el Rigidbody de forma continua
        rb.MoveRotation(rb.rotation * turnRotation);
    }
}

public interface IPlayerContext : IStateContext
{

}
