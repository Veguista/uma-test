using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0, 20)] float maxMovementSpeed = 8;
    [SerializeField][Range(0, 20)] float movementAcceleration = 7;

    Vector3 moveSpeed;

    Transform cameraTransform;

    InputAction playerMoveInputAction;
    InputAction playerVerticalMoveInputAction;

    private void Start()
    {
        cameraTransform = transform.Find("Main Camera");
        playerMoveInputAction = InputSystem.actions.FindAction("Move");
        playerVerticalMoveInputAction = InputSystem.actions.FindAction("VerticalMovement");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 regularMovement = playerMoveInputAction.ReadValue<Vector2>();
        float verticalMovement = playerVerticalMoveInputAction.ReadValue<float>();

        Vector3 movementDir = cameraTransform.forward * regularMovement.y
            + cameraTransform.right * regularMovement.x
            + cameraTransform.up * verticalMovement;

        if(movementDir != Vector3.zero) // We accelerate
        {
            moveSpeed += movementDir * movementAcceleration * Time.deltaTime;
            
            // Clamping the move Speed.
            moveSpeed = Vector3.ClampMagnitude(moveSpeed, maxMovementSpeed);
        }
        else // We reduce speed.
        {
            moveSpeed = moveSpeed.sqrMagnitude <= Mathf.Pow(movementAcceleration, 2) 
                ? Vector3.zero 
                : moveSpeed - moveSpeed.normalized * movementAcceleration * Time.deltaTime;
        }

        transform.position += moveSpeed * Time.deltaTime;
    }
}
