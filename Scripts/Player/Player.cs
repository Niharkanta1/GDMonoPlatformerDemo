using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Player : KinematicBody2D
{
    public AnimationNodeStateMachinePlayback animationState;

    [Export] public int gravity = 1000;
    [Export] public int jumpSpeed = -300;
    [Export] public int walkSpeed = 75;
    [Export] public int lateralFallMovementSpeed = 40;
    [Export] public int dashSpeed = 300;
    [Export] public int maxDash = 1;
    public int numDash;

    public Vector2 velocity;
    public String direction = "right";
    public bool isAttacking = false;
    public bool isDashing = false;

    // Handle slopes
    [Export] public float snapLength = 2;
    [Export] public float floorMaxAngleDegree = 65;
    public Vector2 snapDirection = Vector2.Down;
    public Vector2 snapVector;
    public float floorMaxAngle;
    public Vector2 rigidPush;

    public Sprite sprite;
    public Position2D hitBoxPosition;
    public CollisionShape2D collisionShape2D;
    public CollisionShape2D playerCollisionShape;
    public RayCast2D rayCast;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        animationState = (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
        sprite = GetNode<Sprite>("Sprite");
        hitBoxPosition = GetNode<Position2D>("HitboxPosition");
        rayCast = GetNode<RayCast2D>("RayCast2D");

        playerCollisionShape = GetNode<CollisionShape2D>("CollisionShape2D");

        // Disabling the Hitbox
        collisionShape2D = GetNode<CollisionShape2D>("HitboxPosition/Hitbox/CollisionShape2D");
        collisionShape2D.Disabled = true;

        numDash = maxDash;
        snapVector = snapDirection * snapLength;
        floorMaxAngle = MathUtil.DegreeToRadian(floorMaxAngleDegree);
        rigidPush = new Vector2(255, 50);
    }

    public void UpdateDirection(float inputDirectionX)
    {
        if (inputDirectionX > 0)
            SetDirectionRight();
        else if (inputDirectionX < 0)
            SetDirectionLeft();
    }

    public void SetDirectionLeft()
    {
        direction = "left";
        sprite.FlipH = true;
        hitBoxPosition.RotationDegrees = 180;
    }

    public void SetDirectionRight()
    {
        direction = "right";
        sprite.FlipH = false;
        hitBoxPosition.RotationDegrees = 0;
    }

    public void ApplyGravity(float delta)
    {
        velocity.y += gravity * delta;
    }

    public void OnAttackFinished() => isAttacking = false;
    public void OnDashFinished() => isDashing = false;
    public void ResetDashCounter() => numDash = maxDash;
    public bool HasDashes() => numDash > 0;

    public void RestartLevel()
    {
        // handle restart level after player died
    }
}
