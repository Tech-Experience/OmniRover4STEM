using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GenerateRoverAction;

public class SandBoxBase : MonoBehaviour
{
    // Clear action list
    public void ClearAction()
    {
        actionList.actions.Clear();
    }
}
