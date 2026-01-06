using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField][Range(0, 1000)] float cameraYawSpeed = 100;
    [SerializeField][Range(0, 1000)] float cameraPitchSpeed = 100;

    InputAction cameraMovementInputAction;
    Transform cameraTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cameraTransform = transform.Find("Main Camera");
        cameraMovementInputAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        float yawInput = cameraMovementInputAction.ReadValue<Vector2>().x;
        float pitchInput = -cameraMovementInputAction.ReadValue<Vector2>().y;

        // Separating the rotation of yaw and pitch helps simplify how we manage them.
        transform.rotation = transform.rotation * Quaternion.AngleAxis(yawInput * cameraYawSpeed * Time.deltaTime * Mathf.Deg2Rad, Vector3.up);
        cameraTransform.rotation = cameraTransform.rotation * Quaternion.AngleAxis(pitchInput * cameraPitchSpeed * Time.deltaTime * Mathf.Deg2Rad, Vector3.right);
    }
}
