using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class Door : Area2D
{
    [Export] private PackedScene nextScene;

    public override void _Ready()
    {

    }

    public void OnDoorBodyEnetered(Node body)
    {
        if ("Player".Equals(body.Owner.Name) && nextScene != null)
        {
            ((SceneManager)GetNode("/root/SceneManager")).ChangeScene(nextScene, "LevelFade");
        }
    }

}
