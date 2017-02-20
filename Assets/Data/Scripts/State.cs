using UnityEngine;
using System.Collections;

public abstract class State<T> where T : class
{
    public string state_name;

    public abstract void Enter(T entity);
    public abstract void Update(T entity);
    public abstract void Exit(T entity);

    public string GetStateName() { return state_name; }
}