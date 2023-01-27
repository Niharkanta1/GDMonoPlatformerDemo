using Godot;
using System;
using static Godot.GD;

/*
 * @auther  Nihar
 * @company	DeadW0Lf Games
 */
public enum LogLevel
{
    DEBUG,
    INFO,
    ERROR
}

public static class Logger
{
    private const int LeveStringMax = 6;
    private const int NodeNameMax = 30;

    private static string GetLevelName(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.DEBUG:
                return "DEBUG";
            case LogLevel.INFO:
                return "INFO";
            case LogLevel.ERROR:
                return "ERROR";
            default:
                return "OTHER";
        }
    }

    private static string GetHeader(Node loggee, LogLevel level)
    {
        var level_name = GetLevelName(level);
        return String.Format("{0} {1}", $"[{level_name}]".PadRight(LeveStringMax), $"{loggee.Name}@{loggee.GetType().Name}: {loggee.GetClass()}".PadRight(NodeNameMax));
    }

    private static void Log(Node node, LogLevel level, string message)
    {
        Print($"{GetHeader(node, level)}: {message}");
    }

    public static void Debug(Node node, string message)
    {
        Log(node, LogLevel.DEBUG, message);
    }

    public static void Info(Node node, string message)
    {
        Log(node, LogLevel.INFO, message);
    }

    public static void Error(Node node, string message)
    {
        Log(node, LogLevel.ERROR, message);
    }
}
