using System.Collections.Generic;
using Godot;

/*
 * @auther	Nihar
 * @company	DeadW0Lf Games
 */
public class SoundManager : Node2D
{
    private List<AudioStreamPlayer> musicList = new List<AudioStreamPlayer>();
    private List<AudioStreamPlayer> soundEffectList = new List<AudioStreamPlayer>();

    private float musicScrollVolCurrent;
    private float soundEffectsScrollVolCurrent;
    private float prevMusicVoluIncr;
    private float prevSoundEffectVolIncr;

    public AudioStreamPlayer titleSceneMusic, gameplayMusic, endingMusic;

    public AudioStreamPlayer attackSound, buttonSound, dashSound,
                                deathSound, jumpSound, landSound,
                                levelCompletedSound, logoSound;


    public override void _Ready()
    {
        titleSceneMusic = GetNode<AudioStreamPlayer>("Music/TitleScreenMusic");
        gameplayMusic = GetNode<AudioStreamPlayer>("Music/GameplayMusic");
        endingMusic = GetNode<AudioStreamPlayer>("Music/EndingScreenMusic");

        attackSound = GetNode<AudioStreamPlayer>("SoundEffects/AttackSound");
        buttonSound = GetNode<AudioStreamPlayer>("SoundEffects/ButtonSound");
        dashSound = GetNode<AudioStreamPlayer>("SoundEffects/DashSound");
        deathSound = GetNode<AudioStreamPlayer>("SoundEffects/DeathSound");
        jumpSound = GetNode<AudioStreamPlayer>("SoundEffects/JumpSound");
        landSound = GetNode<AudioStreamPlayer>("SoundEffects/LandSound");
        levelCompletedSound = GetNode<AudioStreamPlayer>("SoundEffects/LevelCompleted");
        logoSound = GetNode<AudioStreamPlayer>("SoundEffects/LogoSound");

        var node = GetNode<Node>("Music");
        for (int i = 0; i < node.GetChildCount(); i++)
        {
            musicList.Add(node.GetChild<AudioStreamPlayer>(i));
        }
        node = GetNode<Node>("SoundEffects");
        for (int i = 0; i < node.GetChildCount(); i++)
        {
            soundEffectList.Add(node.GetChild<AudioStreamPlayer>(i));
        }
    }


    public void UpdateSoundVolume(float value, float volumeRange, string type)
    {
        switch (type)
        {
            case "Music":
                foreach (AudioStreamPlayer music in musicList)
                {
                    music.VolumeDb += value - volumeRange - prevMusicVoluIncr;
                }
                prevMusicVoluIncr = value - volumeRange;
                musicScrollVolCurrent = value;
                break;

            case "SoundEffects":
                foreach (AudioStreamPlayer sound in soundEffectList)
                {
                    sound.VolumeDb += value - volumeRange - prevMusicVoluIncr;
                }
                prevSoundEffectVolIncr = value - volumeRange;
                soundEffectsScrollVolCurrent = value;
                break;
        }

    }

    public void StopAllMusic()
    {
        foreach (AudioStreamPlayer music in musicList)
        {
            if (music.Playing) music.Stop();
        }
    }

}
