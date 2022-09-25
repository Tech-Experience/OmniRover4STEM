using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateRoverAction
{
    public static RoverActionList actionList = new RoverActionList();
    public enum SpeedLevel
    {
        One,
        Two,
        Three
    }

    public static void MoveNorth(float time)
    {
        CreateAction("move_north", time);
    }

    public static void MoveSouth(float time)
    {
        CreateAction("move_south", time);
    }

    public static void MoveEast(float time)
    {
        CreateAction("move_east", time);
    }

    public static void MoveWest(float time)
    {
        CreateAction("move_west", time);
    }

    public static void MoveNorthEast(float time)
    {
        CreateAction("move_northeast", time);
    }

    public static void MoveNorthWest(float time)
    {
        CreateAction("move_northwest", time);
    }

    public static void MoveSouthEast(float time)
    {
        CreateAction("move_southeast", time);
    }

    public static void MoveSouthWest(float time)
    {
        CreateAction("move_southwest", time);
    }

    public static void RotateRight(float time)
    {
        CreateAction("rotate_right", time);
    }

    public static void RotateLeft(float time)
    {
        CreateAction("rotate_left", time);
    }

    public static void SetSpeed(SpeedLevel level)
    {
        float speedLevel = 1f;
        if (level == SpeedLevel.One) speedLevel = 1;
        else if (level == SpeedLevel.Two) speedLevel = 2;
        else if (level == SpeedLevel.Three) speedLevel = 3;

        CreateAction("set_speed", speedLevel);
    }

    private static void CreateAction(string action, float value)
    {
        RoverAction roverAction = new RoverAction() { action = action, value = value };
        actionList.actions.Add(roverAction);
    }
}

