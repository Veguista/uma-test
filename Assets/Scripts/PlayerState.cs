using UnityEngine;

public class PlayerState : MonoBehaviour
{
    // Using Singleton pattern here cause it is not specified that the game might be multiplayer.
    // Otherwise, this should change.
    public static PlayerState Instance { get; private set; }

    // Our State Machine
    public delegate void PlayerChangedStateDelegate(EPlayerStates _newPlayerState);
    public PlayerChangedStateDelegate OnPlayerChangedState;

    private EPlayerStates _states = EPlayerStates.Normal;
    public EPlayerStates States 
    { 
        get => _states;
        set
        {
            if(_states != value)
            {
                _states = value;

                if (OnPlayerChangedState != null)
                {
                    OnPlayerChangedState.Invoke(_states);
                }
            }
        } 
    }


    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("Trying to set static instance but it wasn't null. You probably have 2 instances of the PlayerState class. This is not allowed.");
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance)
        {
            Instance = null;
        }
    }
}

public enum EPlayerStates
{
    Normal,
    HoldingObject,
    Paused
}
