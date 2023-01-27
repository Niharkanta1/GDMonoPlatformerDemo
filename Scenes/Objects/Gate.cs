using System;
using System.Collections;
using Godot;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class Gate : StaticBody2D
{
    private AnimatedSprite animatedSprite;
    private CollisionShape2D collisionShape2D;

    private int state;
    // States: 0 - Close, 1 - Open, 2 - Idle
    private bool isClosed = true;
    private bool isClosing = false, isOpening = false;

    [Signal] public delegate void OperationFinished(bool value);

    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
        state = 2;
    }

    public override void _Process(float delta)
    {
        switch (state)
        {
            case 2:
                animatedSprite.Play("Idle");
                break;

            case 0:
                if (isClosed) break;
                collisionShape2D.Disabled = false;
                animatedSprite.Play("Close");
                //await ToSignal(animatedSprite, "animation_finished");
                isClosing = true;
                break;

            case 1:
                if (!isClosed) break;
                animatedSprite.Play("Open");
                //await ToSignal(animatedSprite, "animation_finished");
                isOpening = true;
                break;

            default:
                break;
        }
    }

    public void OpenGate()
    {
        GD.Print("Open gate");
        if (isClosed)
            state = 1;
    }

    public void CloseGate()
    {
        GD.Print("Close gate");
        if (!isClosed)
            state = 0;
    }

    public void OnAnimatedSpriteAnimationFinished()
    {
        if (isClosing)
        {
            isClosing = false;
            isClosed = true;
            EmitSignal(nameof(OperationFinished), false); // OpenGate = False
        }

        if (isOpening)
        {
            isOpening = false;
            collisionShape2D.Disabled = true;
            isClosed = false;
            EmitSignal(nameof(OperationFinished), true); // OpenGate = True
        }

        if (state == 0)
            state = 2;
    }
}

