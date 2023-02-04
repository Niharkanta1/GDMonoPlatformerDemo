using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class SceneManager : CanvasLayer
{
    [Export] private Color darkColor = new Color(20, 50, 85, 255);
    [Export] private Color whiteColor = new Color(255, 255, 255, 255);
    [Export] bool testing = false;
    [Export] PackedScene testingScene;

    private AnimationPlayer animationPlayer;
    private ColorRect colorRect;

    PackedScene logoScreen = ResourceLoader.Load<PackedScene>("res://Scenes/UI/LogoScene.tscn");

    private string lastPlayedAnim = null;

    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        colorRect = GetNode<ColorRect>("ColorRect");
        if (testing)
        {
            ChangeScene(testingScene, "DarkFade");
            return;
        }
        ChangeScene(logoScreen, "DarkFade");

    }

    public void ChangeScene(PackedScene newScene, string animation = null)
    {
        if (animation != null)
        {
            ResetColorRect(animation);
            animationPlayer.Play(animation);
        }

        var value = GetTree().ChangeSceneTo(newScene);
        lastPlayedAnim = animation;
    }

    public float GetAnimDuration()
    {
        if (lastPlayedAnim == null)
            return 0;

        return animationPlayer.GetAnimation(lastPlayedAnim).Length;
    }

    public void ResetColorRect(string animation)
    {
        switch (animation)
        {
            case "DarkFade":
                colorRect.Color = darkColor;
                break;

            case "LightFade":
                colorRect.Color = whiteColor;
                break;

            case "LevelFade":
                colorRect.Color = whiteColor;
                break;
        }
    }

}
