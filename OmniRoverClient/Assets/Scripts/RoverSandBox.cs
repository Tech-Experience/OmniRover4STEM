using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GenerateRoverAction;

public class RoverSandBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveRover();
    }
    

    public void MoveRoverTest()
    {
        SetSpeed(SpeedLevel.Two);
        MoveForward(1f);
        MoveBackward(1f);
        MoveRight(1f);
        MoveLeft(1f);
        //SetSpeed(SpeedLevel.One);
        MoveForwardRight(1f);
        MoveForwardLeft(1f);
        //SetSpeed(SpeedLevel.Two);
        MoveBackwardRight(1f);
        MoveBackwardLeft(1f);
        //SetSpeed(SpeedLevel.Three);
        RotateLeft(0.5f);
        RotateRight(0.5f);

    }

    private void MoveRover()
    {
        RotateLeft(0.5f);
        MoveForward(0.75f);
        RotateRight(0.5f);
        MoveForward(1.0f);
        RotateRight(0.35f);
        MoveForward(1f);
        RotateLeft(0.35f);
        MoveForward(3f);
    }
}
