using UnityEngine;

public abstract class State
{
    public AI owner;
    public virtual void Enter() { }
    public virtual void Execute() { }
    public virtual void Exit() { }
}