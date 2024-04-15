using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine
{
    public BossStates currentState { get; private set; }
    public void Initialize(BossStates state)
    {
        currentState = state;
        currentState.Enter();
    }
    public void ChangeState(BossStates state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    } 
}
