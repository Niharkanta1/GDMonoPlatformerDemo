using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class LogoScreen : TextureRect
{
    [Export] PackedScene nextScene;

    SoundManager soundManager;
    SceneManager sceneManager;

    private Timer timer;

    private float duration = 1;

    public override void _Ready()
    {
        soundManager = GetNode<SoundManager>("/root/SoundManager");
        sceneManager = GetNode<SceneManager>("/root/SceneManager");
        timer = GetNode<Timer>("Timer");

        soundManager.StopAllMusic();
        soundManager.logoSound.Play();
        timer.WaitTime = sceneManager.GetAnimDuration() + duration;
        timer.Start();
    }

    public void OnTimerTimeout()
    {
        sceneManager.ChangeScene(nextScene, "LightFade");
    }


}
