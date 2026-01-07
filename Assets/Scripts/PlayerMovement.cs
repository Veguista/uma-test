using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField][Range(0, 20)] float maxMovementSpeed = 8;
    [SerializeField][Range(0, 20)] float movementAcceleration = 7;
    [SerializeField][Range(0, 5)] float movementResistance = 1.3f;

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
        // Applying move resistance.
        moveSpeed *= 1.0f - Mathf.Clamp01(movementResistance * Time.deltaTime);

        // New move input
        Vector2 regularMovement = playerMoveInputAction.ReadValue<Vector2>();
        float verticalMovement = playerVerticalMoveInputAction.ReadValue<float>();

        Vector3 movementDir = cameraTransform.forward * regularMovement.y
            + cameraTransform.right * regularMovement.x
            + cameraTransform.up * verticalMovement;

        // We accelerate
        moveSpeed += movementDir * movementAcceleration * Time.deltaTime;
            
        // Clamping the move Speed.
        moveSpeed = Vector3.ClampMagnitude(moveSpeed, maxMovementSpeed);
        
        transform.position += moveSpeed * Time.deltaTime;
    }
}
