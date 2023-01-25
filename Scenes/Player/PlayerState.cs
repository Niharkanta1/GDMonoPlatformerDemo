using Godot;
using System;
using System.Diagnostics;

public class PlayerState : State
{
    public Player player;

    public override async void _Ready()
    {
        await ToSignal(Owner, "ready");
        player = (Player)Owner;
        Debug.Assert(player != null);
    }
}
