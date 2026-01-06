using UnityEngine;

public interface IInteractable
{
    /// <summary>
    /// Tries to interact with the element.
    /// </summary>
    /// <returns>Whether the element accepts the interaction.</returns>
    public bool TryToInteract();
}
