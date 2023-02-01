using Godot;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public class RigidBox : RigidBody2D
{
    [Export] private float maxSpeed = 10.0f;

    public override void _IntegrateForces(Physics2DDirectBodyState state)
    {
        if (this.LinearVelocity.Length() > maxSpeed)
        {
            var direction = this.LinearVelocity.Normalized();
            this.LinearVelocity = direction * maxSpeed;
        }
    }
}
