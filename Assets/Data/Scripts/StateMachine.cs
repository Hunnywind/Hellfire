using UnityEngine;
using System.Collections;


public class StateMachine<T> where T : class
{
    private T owner_entity;
    private State<T> current_state;

    public void Init(T _owner, State<T> initState)
    {
        owner_entity = _owner;
        ChangeState(initState);
    }
    public void Update()
    {
        if (current_state != null)
        {
            current_state.Update(owner_entity);
            return;
        }
    }
    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;

        if (current_state != null)
        {

            current_state.Exit(owner_entity);
        }

        current_state = newState;
        current_state.Enter(owner_entity);
    }
    public void Set_CurrentState(State<T> state) { current_state = state; }

    public State<T> Get_CurrentState() { return current_state; }
}