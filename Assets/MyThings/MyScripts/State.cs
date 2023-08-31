using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected StateMachine sm;
    protected GameObject gameObject;
    
    public State(GameObject gameObject, StateMachine sm)
    {
        this.gameObject = gameObject;
        this.sm = sm;
    }

    public virtual void Enter()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void FixedUpdate()
    {

    }
}
