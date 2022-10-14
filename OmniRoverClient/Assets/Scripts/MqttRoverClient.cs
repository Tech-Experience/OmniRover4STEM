using M2MqttUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MqttRoverClient : M2MqttUnityClient
{
    [SerializeField]
    private MqttTopic publishTopic;
    public string PublishTopic
    {
        get => publishTopic.topic;
    }

    private Coroutine publishActionListFineCoroutine;
    public void PublishRoverActionList()
    {
        PublishRoverActionListAll();
        //PublishRoverActionFine();
    }


    public void StopRover()
    {
        RoverAction stopAction = new RoverAction() { action = "stop", value = 0 };
        RoverActionList actionList = new RoverActionList();
        actionList.actions.Add(stopAction);

        string payload = JsonUtility.ToJson(actionList);

        if (publishActionListFineCoroutine != null) StopCoroutine(publishActionListFineCoroutine);
        PublishPayload(payload, PublishTopic);
    }

    /// <summary>
    /// Publish all RoverAction at one
    /// </summary>
    private void PublishRoverActionListAll()
    {
        GenerateRoverAction.SetSpeed(GenerateRoverAction.SpeedLevel.Two); // reset speed at the end
        string payload = JsonUtility.ToJson(GenerateRoverAction.actionList);
        PublishPayload(payload, PublishTopic);
    }

    private void PublishRoverActionFine()
    {
        if (publishActionListFineCoroutine != null) StopCoroutine(publishActionListFineCoroutine);
        publishActionListFineCoroutine = StartCoroutine(PublishRoverActionListFine(GenerateRoverAction.actionList));
    }

    /// <summary>
    /// Publish RoverActionList 1 action at a time
    /// </summary>
    /// <param name="actionList"></param>
    /// <returns></returns>
    private IEnumerator PublishRoverActionListFine(RoverActionList actionList)
    {
        List<RoverAction> roverActionList = actionList.actions;
        List<RoverActionList> fineActionList = new List<RoverActionList>();
        foreach(RoverAction action in roverActionList)
        {
            RoverActionList newActionList = new RoverActionList();
            newActionList.actions.Add(action);
            fineActionList.Add(newActionList);
        }

        foreach(RoverActionList list in fineActionList)
        {
            string payload = JsonUtility.ToJson(list);
            PublishPayload(payload, PublishTopic);
            if (list.actions[0].action == "set_speed") continue;
            yield return new WaitForSeconds(list.actions[0].value);
        }
    }

    private void PublishPayload(string payload, string topic)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(payload));
        Debug.Log("Rover Action published");
        Debug.Log($"topic: {topic}, payload published : {payload}");
    }
}
