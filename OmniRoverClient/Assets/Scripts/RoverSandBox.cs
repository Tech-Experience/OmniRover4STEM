using UnityEngine;
using static GenerateRoverAction;

public class RoverSandBox : SandBoxBase
{
    // Mystery function. Try running it!
    public void MoveSandBox()
    {
        MoveForward(1);
        MoveRight(1);
        MoveBackward(1);
        MoveLeft(1);
    }
}
