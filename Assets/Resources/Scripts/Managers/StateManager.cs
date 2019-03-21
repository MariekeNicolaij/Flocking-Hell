using UnityEngine;
using System.Collections;

public class StateManager
{
    //public static StateManager instance;
    public AI owner;
    public State currentState;

    State defaultState;

    public StateManager(AI owner, State defaultState)
    {
        this.owner = owner;
        this.defaultState = defaultState;
        ChangeState(defaultState);
    }

    public void Start()
    {
        //instance = this;
    }

    public void Update()
    {
        currentState.Execute();
    }

    public void ChangeState(State state)
    {
        if (state != null)
        {
            state.owner = owner;
            if (currentState != null)
                currentState.Exit();
            currentState = state;
            currentState.Enter();
        }
    }

    public void ChangeToDefault()
    {
        ChangeState(defaultState);
    }
}