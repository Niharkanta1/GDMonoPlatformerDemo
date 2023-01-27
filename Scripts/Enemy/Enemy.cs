using Godot;
using System;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Enemy : KinematicBody2D
{
    [Export] private float gravity = 1000;
    [Export] private int walkSpeed = 75;

    private Vector2 velocity;
    private String direction = "right";

    private States state;
    enum States { Walk, Death };

    private RandomNumberGenerator rng;
    private int inputDirectionX;
    private Timer moveTimer;
    private AnimatedSprite animatedSprite;
    private CollisionShape2D collisionShape2D;

    private bool isDying = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        rng = new RandomNumberGenerator();
        rng.Randomize();
        inputDirectionX = 1;
        state = States.Walk;
        moveTimer = GetNode<Timer>("MoveTimer");
        moveTimer.Start();
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    // Runs 60 times per seconds
    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
        switch (state)
        {
            case States.Walk:
                animatedSprite.Play("Walk");
                UpdateDirection(inputDirectionX);
                velocity.x = walkSpeed * inputDirectionX;
                ApplyGravity(delta);
                velocity = MoveAndSlide(velocity, Vector2.Up);

                // handle collisions here

                break;

            case States.Death:
                animatedSprite.Play("Death");
                collisionShape2D.Disabled = true;
                //await ToSignal(animatedSprite, "animation_finished");
                isDying = true;
                break;

            default:
                break;
        }
    }

    private int GetRandomDirection()
    {
        int[] result = { -1, 1 };
        return result[rng.RandiRange(0, 1)];
    }

    private void SetDirectionRight()
    {
        direction = "right";
        animatedSprite.FlipH = false;
    }

    private void SetDirectionLeft()
    {
        direction = "left";
        animatedSprite.FlipH = true;
    }

    private void UpdateDirection(int inputDirectionX)
    {
        if (inputDirectionX > 0)
            SetDirectionRight();
        else if (inputDirectionX < 0)
            SetDirectionLeft();
    }

    private void ApplyGravity(float delta)
    {
        velocity.y += gravity * delta;
    }

    // Signals

    public void OnMoveTimerTimeout()
    {
        inputDirectionX = GetRandomDirection();
    }

    public void OnHurtboxAreaEntered(Area2D other)
    {
        if (other.Owner.Name == "Player")
        {
            state = States.Death;
        }
    }

    // Signals

    public void OnAnimatedSpriteAnimationFinished()
    {
        if (isDying)
        {
            isDying = false;
            QueueFree();
        }
    }

}

