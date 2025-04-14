using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 3f;

    Vector2 movement;

    Rigidbody m_Rigidbody;



    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>(); // Gives the vector of pressed key
        Debug.Log(movement);
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 currentPosition = m_Rigidbody.position;
        Vector3 movementDirection = new Vector3(movement.x, 0, movement.y);
        Vector3 newPosition = currentPosition + movementDirection * Time.fixedDeltaTime * movementSpeed;

        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        m_Rigidbody.MovePosition(newPosition);
    }
}
