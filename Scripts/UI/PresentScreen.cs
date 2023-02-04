using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class PresentScreen : TextureRect
{
    [Export] private PackedScene nextScene;
    private float duration = 1;

    SceneManager sceneManager;
    Timer timer;

    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        timer = GetNode<Timer>("Timer");

        timer.WaitTime = sceneManager.GetAnimDuration() + duration;
        timer.Start();
    }

    public void OnTimerTimeout()
    {
        sceneManager.ChangeScene(nextScene, "LightFade");
        ((SoundManager)GetNode("/root/SoundManager")).gameplayMusic.Play(); // TO-DO: change it to Title Music
    }
}
