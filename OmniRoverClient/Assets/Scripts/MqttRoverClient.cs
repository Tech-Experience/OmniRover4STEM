using M2MqttUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MqttRoverClient : M2MqttUnityClient
{
    [SerializeField]
    private string publishTopic;
    public string PublishTopic
    {
        get => publishTopic;
    }

    public void PublishRoverActionList()
    {
        string payload = JsonUtility.ToJson(GenerateRoverAction.actionList);
        client.Publish(publishTopic, System.Text.Encoding.UTF8.GetBytes(payload));
        Debug.Log("Rover Action published");
        Debug.Log($"payload published : {payload}");
    }


    public void StopRover()
    {
        RoverAction stopAction = new RoverAction() { action = "stop", value = 0 };
        RoverActionList actionList = new RoverActionList();
        actionList.actions.Add(stopAction);

        string payload = JsonUtility.ToJson(actionList);
        client.Publish(publishTopic, System.Text.Encoding.UTF8.GetBytes(payload));
        Debug.Log("Rover Stop Action published");
        Debug.Log($"payload published : {payload}");
    }
}
