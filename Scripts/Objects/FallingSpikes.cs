using System;
using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class FallingSpikes : KinematicBody2D
{
    private RayCast2D rayCast;

    [Export] private float gravity = 1000.0f;
    [Export] private float rayCastLength = 50f;

    private bool isStuck = true;
    private Vector2 velocity = Vector2.Zero;

    public override void _Ready()
    {
        rayCast = GetNode<RayCast2D>("RayCast2D");
        rayCast.CastTo = new Vector2(0, rayCastLength);
    }


    public override void _PhysicsProcess(float delta)
    {
        if (!isStuck)
        {
            ApplyGravity(delta);
            velocity = MoveAndSlide(velocity, Vector2.Up);
        }

        if (rayCast.IsColliding() && rayCast.CollideWithBodies)
        {
            if ("Player".Equals(rayCast.GetCollider().GetType().Name))
                isStuck = false;
        }

        // Handle Collisions
        if (GetSlideCount() > 0)
        {
            for (int i = 0; i < GetSlideCount(); i++)
            {
                var collision = GetSlideCollision(i);
                var collider = collision.Collider;
                if (collider.GetType().Name.Equals("TileMap"))
                {
                    velocity = Vector2.Zero;
                    isStuck = true;
                }
                else if (collider.GetType().Name.Equals("Player"))
                {
                    StateMachine sm = ((Node)collider).GetNode<StateMachine>("StateMachine");
                    sm.TransitionTo("Death");
                }
            }
        }

    }

    private void ApplyGravity(float delta)
    {
        velocity.y += gravity * delta;
        velocity.x = 0;  // Remove horrizontal movement completely
    }
}
