using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField][Range(0, 1000)] float interactRange = 100.0f;

    InputAction interactAction;

    const int terrainLayerMask = 3;
    const int shapesLayerMask = 6;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Interact");
        interactAction.performed += TryToInteract;
    }

    private void OnDestroy()
    {
        interactAction.performed -= TryToInteract;
    }


    void TryToInteract(InputAction.CallbackContext ctx)
    {
        RaycastHit rayHit;

        if (!Physics.Raycast(transform.position, transform.forward, out rayHit, interactRange, shapesLayerMask))
        {
            // Didn't hit anything.
            return;
        }

        IInteractable interactInterface;

        if (!rayHit.transform.TryGetComponent<IInteractable>(out interactInterface))
        {
            // Object cannot be interacted with.
            return;
        }

        if (interactInterface.TryToInteract())
        {
            // Interaction accepted.

        }
        else // Interaction rejected.
        {

        }
    }
}
