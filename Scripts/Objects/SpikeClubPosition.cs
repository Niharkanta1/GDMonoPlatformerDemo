using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class SpikeClubPosition : Position2D
{

    [Export] private float rotationSpeed = 50.0f;
    [Export] private bool left, right, up, down;

    private float startingRotation, endingRotation;
    private int direction = 1;

    public override void _Ready()
    {
        if (left)
        {
            startingRotation = 0;
            endingRotation = 180;
        }
        if (right)
        {
            startingRotation = 180;
            endingRotation = 360;
        }
        if (up)
        {
            startingRotation = 90;
            endingRotation = 270;
        }
        if (down)
        {
            startingRotation = -90;
            endingRotation = 90;
        }

        this.RotationDegrees = startingRotation;
    }

    public override void _PhysicsProcess(float delta)
    {
        if (this.RotationDegrees < startingRotation)
            direction = 1;
        else if (this.RotationDegrees > endingRotation)
            direction = -1;

        this.RotationDegrees += rotationSpeed * direction * delta;
    }


}
