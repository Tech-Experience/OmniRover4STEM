using UnityEngine;
using static GenerateRoverAction;

public class RoverSandBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MoveRoverMystery();
    }

    private void MoveRoverMystery()
    {
        MoveForward(1);
        MoveRight(1);
        MoveBackward(1);
        MoveLeft(1);
    }

}
