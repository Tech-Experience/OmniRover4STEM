using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GenerateRoverAction
{
    public static RoverActionList actionList = new RoverActionList();
    public const float MAX_TIME = 10f;
    public enum SpeedLevel
    {
        One,
        Two,
        Three
    }

    public static void MoveForward(float time)
    {
        CreateAction("move_forward", time);
    }

    public static void MoveBackward(float time)
    {
        CreateAction("move_backward", time);
    }

    public static void MoveRight(float time)
    {
        CreateAction("move_right", time);
    }

    public static void MoveLeft(float time)
    {
        CreateAction("move_left", time);
    }

    public static void MoveForwardRight(float time)
    {
        CreateAction("move_forwardright", time);
    }

    public static void MoveForwardLeft(float time)
    {
        CreateAction("move_forwardleft", time);
    }

    public static void MoveBackwardRight(float time)
    {
        CreateAction("move_backwardright", time);
    }

    public static void MoveBackwardLeft(float time)
    {
        CreateAction("move_backwardleft", time);
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

    public static void Stop(float time)
    {
        CreateAction("stop", time);
    }

    public static void MoveSquare(float size)
    {
        MoveForward(size);
        MoveRight(size);
        MoveBackward(size);
        MoveLeft(size);
    }

    public static void MoveCircle(int iteration, float foward = 0.2f, float rotate = 0.2f)
    {
        for (int i = 0; i < iteration; i++)
        {
            MoveForward(foward);
            RotateRight(rotate);
        }
    }

    private static void CreateAction(string action, float value)
    {
        value = Mathf.Min(value, MAX_TIME);
        RoverAction roverAction = new RoverAction() { action = action, value = value };
        actionList.actions.Add(roverAction);
    }
}

