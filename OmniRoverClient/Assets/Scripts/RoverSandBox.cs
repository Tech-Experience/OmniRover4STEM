using UnityEngine;
using static GenerateRoverAction;

public class RoverSandBox : SandBoxBase
{
    // Mystery function. Try running it!
    public void MoveSandBox()
    {
        
    }

    public void MoveSandboxSquare()
    {
        MoveForward(1);
        MoveRight(1);
        MoveBackward(1);
        MoveLeft(1);
    }

    public void MoveSandBoxCircle()
    {
        MoveCircle(10);
    }

    public void MoveSandMoveTriangle()
    {
        MoveForwardRight(0.75f);
        MoveBackwardRight(0.75f);
        MoveLeft(1.5f);
    }
}
