using Godot;
using System;

public class StateMachine : Node
{
    [Export] private NodePath initialState;
    [Signal] public delegate void Transitioned(String signalname);

    State state;

    public override async void _Ready()
    {
        state = GetNode<State>(initialState);
        await ToSignal(Owner, "ready");
        foreach (PlayerState child in GetChildren())
        {
            child.PlayerStateMachine = this;
        }

        state.Enter();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        state.HandleInput(@event);
    }

    public override void _Process(float delta)
    {
        state.Update(delta);
    }

    public override void _PhysicsProcess(float delta)
    {
        state.PhysicsUpdate(delta);
    }

    public void TransitionTo(String targetStateName)
    {
        if (!HasNode(targetStateName)) return;

        state.Exit();
        state = GetNode<State>(targetStateName);
        state.Enter();
        EmitSignal("Transitioned", state.Name);
    }

}
