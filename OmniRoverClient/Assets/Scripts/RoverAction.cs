using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoverAction
{
    public string action;
    public float value;
}

[System.Serializable]
public class RoverActionList
{
    public List<RoverAction> actions = new List<RoverAction>();
}


