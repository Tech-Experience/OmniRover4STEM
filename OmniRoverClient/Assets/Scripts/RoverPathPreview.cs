using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoverPathPreview : MonoBehaviour
{
    public Transform rover;

    [SerializeField]
    private float speedLevel1 = 1;
    [SerializeField]
    private float speedLevel2 = 2;
    [SerializeField]
    private float speedLevel3 = 3;

    private Dictionary<string, Func<float, IEnumerator>> dispatch = new Dictionary<string, Func<float, IEnumerator>>();
    private float currentSpeed;
    public void Start()
    {
        dispatch.Add("move_north", MoveNorth);
        dispatch.Add("move_south", MoveSouth);
        dispatch.Add("move_east", MoveEast);
        dispatch.Add("move_west", MoveWest);
        dispatch.Add("move_northeast", MoveNorthEast);
        dispatch.Add("move_northwest", MoveNorthWest);
        dispatch.Add("move_southeast", MoveSouthEast);
        dispatch.Add("move_southwest", MoveSouthWest);
        dispatch.Add("rotate_left", RotateLeft);
        dispatch.Add("rotate_right", RotateRight);
        dispatch.Add("set_speed", SetSpeed);

        currentSpeed = speedLevel1;
    }
    public void PreviewPath()
    {
        string json = JsonUtility.ToJson(GenerateRoverAction.actionList);
        RoverActionList actionList = JsonUtility.FromJson<RoverActionList>(json);
        StartCoroutine(StartPath(actionList));
    }

    private IEnumerator StartPath(RoverActionList actionList)
    {
        foreach (RoverAction roverAction in actionList.actions)
        {
            yield return dispatch[roverAction.action](roverAction.value);
        }
    }
    private IEnumerator MoveNorth(float time)
    {

        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * Vector2.up);
        });
        
    }

    private IEnumerator MoveSouth(float time)
    {
        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * Vector2.down);
        });
    }

    private IEnumerator MoveEast(float time)
    {
        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * Vector2.right);
        });
    }

    private IEnumerator MoveWest(float time)
    {
        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * Vector2.left);
        });
    }

    private IEnumerator MoveNorthEast(float time)
    {
        Vector2 direction = new Vector2(1, 1);

        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * direction.normalized);
        });
    }

    private IEnumerator MoveNorthWest(float time)
    {
        Vector2 direction = new Vector2(-1, 1);

        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * direction.normalized);
        });
    }

    private IEnumerator MoveSouthEast(float time)
    {
        Vector2 direction = new Vector2(1, -1);

        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * direction.normalized);
        });
    }

    private IEnumerator MoveSouthWest(float time)
    {
        Vector2 direction = new Vector2(-1, -1);

        yield return Countdown(time, () =>
        {
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * direction.normalized);
        });
    }

    private IEnumerator RotateRight(float time)
    {
        yield return Rotate(time, -Vector3.forward);
    }

    private IEnumerator RotateLeft(float time)
    {
        yield return Rotate(time, Vector3.forward);
    }

    private IEnumerator SetSpeed(float level)
    {
        currentSpeed = level;
        return null;
    }

    private IEnumerator Countdown(float time, Action action)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            normalizedTime += Time.deltaTime / time;
            action();
            yield return null;
        }
    }

    private IEnumerator Rotate(float time, Vector3 direction)
    {        
        while (time > 0)     //While the time is more than zero...
        {
            rover.Rotate(direction, Time.deltaTime * currentSpeed * 30);
            time -= Time.deltaTime;
            yield return null;
        }
    }
}
