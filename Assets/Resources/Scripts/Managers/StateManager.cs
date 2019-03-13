using UnityEngine;
using System.Collections;

public class StateManager
{
    public static StateManager instance;
    public GameObject owner;
    public State currentState;

    private State defaultState;

    public StateManager(GameObject owner, State defaultState)
    {
        this.owner = owner;
        this.defaultState = defaultState;
    }

    public void Start()
    {
        instance = this;
        ChangeState(defaultState);
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