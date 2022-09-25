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
    

    public void MoveRover()
    {
        SetSpeed(SpeedLevel.Two);
        MoveNorth(1f);
        MoveSouth(1f);
        MoveEast(1f);
        MoveWest(1f);
        //SetSpeed(SpeedLevel.One);
        MoveNorthEast(1f);
        MoveNorthWest(1f);
        //SetSpeed(SpeedLevel.Two);
        MoveSouthEast(1f);
        MoveSouthWest(1f);
        //SetSpeed(SpeedLevel.Three);
        RotateLeft(0.5f);
        RotateRight(0.5f);

    }
}
