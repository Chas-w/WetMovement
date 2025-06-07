using UnityEngine;
using System;
using System.Collections.Generic;
//tutorial followed: https://www.youtube.com/watch?v=qsIiFsddGV4
public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    Dictionary<EState , BaseState<EState>> States = new Dictionary<EState, BaseState<EState>> ();

    protected BaseState<EState> CurrentState;
    protected bool IsTransitioningState = false; 
    void Start() 
    { 
        CurrentState.EnterState();
    }
    void Update() 
    {

        EState nextStateKey = CurrentState.GetNextState();
        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
        {
            if (nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }
            else
            {
                TransitionToState(nextStateKey);
            }
        }
    }

    public void TransitionToState(EState stateKey)
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false; 
    }
    void OnTriggerEnter(Collider other) 
    {
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerStay(Collider other) 
    {
        CurrentState.OnTriggerStay(other);
    }
    void OnTriggerExit(Collider other) 
    {
        CurrentState.OnTriggerExit(other);
    }

}
