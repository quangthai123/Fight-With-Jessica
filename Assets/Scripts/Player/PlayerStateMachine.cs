using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStates currentState;

    public void Initialize(PlayerStates state)
    {
        currentState = state;
        currentState.Enter();
    }

    public void ChangeState(PlayerStates state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
