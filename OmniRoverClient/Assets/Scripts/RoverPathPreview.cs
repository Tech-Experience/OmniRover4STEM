using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoverPathPreview : MonoBehaviour
{
    public Transform rover;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    [SerializeField]
    private float speedLevel1 = 1;
    [SerializeField]
    private float speedLevel2 = 2;
    [SerializeField]
    private float speedLevel3 = 3;

    private Dictionary<string, Func<float, IEnumerator>> dispatch = new Dictionary<string, Func<float, IEnumerator>>();
    private float currentSpeed;

    private Coroutine previewPathCoroutine;
    public void Start()
    {
        originalPosition = rover.position;
        originalRotation = rover.rotation;

        dispatch.Add("move_forward", MoveNorth);
        dispatch.Add("move_backward", MoveSouth);
        dispatch.Add("move_right", MoveEast);
        dispatch.Add("move_left", MoveWest);
        dispatch.Add("move_forwardright", MoveNorthEast);
        dispatch.Add("move_forwardleft", MoveNorthWest);
        dispatch.Add("move_backwardright", MoveSouthEast);
        dispatch.Add("move_backwardleft", MoveSouthWest);
        dispatch.Add("rotate_left", RotateLeft);
        dispatch.Add("rotate_right", RotateRight);
        dispatch.Add("set_speed", SetSpeed);
        dispatch.Add("stop", StopRover);
        dispatch.Add("wait", StopRover);
        dispatch.Add("color_init", ColorInit);
        dispatch.Add("color_change", ColorChange);
        currentSpeed = speedLevel1;
    }

    public void StopRoverPreview()
    {
        if (previewPathCoroutine != null) StopCoroutine(previewPathCoroutine);
    }

    public void PreviewPath()
    {
        string json = JsonUtility.ToJson(GenerateRoverAction.actionList);
        RoverActionList actionList = JsonUtility.FromJson<RoverActionList>(json);

        rover.rotation = originalRotation;
        rover.position = originalPosition;
        if (previewPathCoroutine != null) StopCoroutine(previewPathCoroutine);
        previewPathCoroutine = StartCoroutine(StartPath(actionList));
    }

    private bool colorChangeInit;
    private int rgbCounter;
    private Color currentColor = new Color();
    private IEnumerator ColorInit(float arg)
    {
        if (arg != GenerateRoverAction.COLOR_CHANGE_FLAG) return null;
        colorChangeInit = true;
        return null;
    }

    private IEnumerator ColorChange(float value)
    {
        if (!colorChangeInit) return null;
        currentColor.a = 1;
        float newValue = value / byte.MaxValue;
        if(rgbCounter == 0)
        {
            currentColor.r = newValue;
        } 
        else if (rgbCounter == 1)
        {
            currentColor.g = newValue;
        } 
        else if (rgbCounter == 2)
        {
            currentColor.b = newValue;
            colorChangeInit = false;
            rgbCounter = 0;
            
            rover.gameObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", currentColor);
            return null;
        } 
        else
        {
            throw new Exception("Invalid color counter");
        }
        rgbCounter++;
        return null;
    }

    private IEnumerator StartPath(RoverActionList actionList)
    {
        foreach (RoverAction roverAction in actionList.actions)
        {
            yield return dispatch[roverAction.action](roverAction.value);
        }
    }
    
    private IEnumerator StopRover(float time)
    {
        yield return Countdown(time, () =>
        {
            // No action for {time} seconds
        });
    }

    private IEnumerator MoveNorth(float time)
    {

        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(Vector2.up);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
        });
        
    }

    private IEnumerator MoveSouth(float time)
    {
        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(Vector2.down);
            rover.localPosition+= currentSpeed * Time.deltaTime * worldDir;
        });
    }

    private IEnumerator MoveEast(float time)
    {
        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(Vector2.right);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
        });
    }

    private IEnumerator MoveWest(float time)
    {
        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(Vector2.left);
            rover.position += (Vector3)(currentSpeed * Time.deltaTime * worldDir);
        });
    }

    private IEnumerator MoveNorthEast(float time)
    {
        Vector2 direction = new Vector2(1, 1);

        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(direction);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
        });
    }

    private IEnumerator MoveNorthWest(float time)
    {
        Vector2 direction = new Vector2(-1, 1);

        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(direction);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
        });
    }

    private IEnumerator MoveSouthEast(float time)
    {
        Vector2 direction = new Vector2(1, -1);

        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(direction);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
        });
    }

    private IEnumerator MoveSouthWest(float time)
    {
        Vector2 direction = new Vector2(-1, -1);

        yield return Countdown(time, () =>
        {
            Vector3 worldDir = rover.TransformDirection(direction);
            rover.position += currentSpeed * Time.deltaTime * worldDir;
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
            rover.Rotate(direction, Time.deltaTime * currentSpeed * 90);
            time -= Time.deltaTime;
            yield return null;
        }
    }
}
