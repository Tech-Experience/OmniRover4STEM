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

    private Coroutine publishActionListFineCoroutine;
    public void PublishRoverActionList()
    {
        //PublishRoverActionListAll();
        PublishRoverActionFine();
    }


    public void StopRover()
    {
        RoverAction stopAction = new RoverAction() { action = "stop", value = 0 };
        RoverActionList actionList = new RoverActionList();
        actionList.actions.Add(stopAction);

        string payload = JsonUtility.ToJson(actionList);

        if (publishActionListFineCoroutine != null) StopCoroutine(publishActionListFineCoroutine);
        PublishPayload(payload, publishTopic);
    }

    /// <summary>
    /// Publish all RoverAction at one
    /// </summary>
    private void PublishRoverActionListAll()
    {
        string payload = JsonUtility.ToJson(GenerateRoverAction.actionList);
        client.Publish(publishTopic, System.Text.Encoding.UTF8.GetBytes(payload));
        Debug.Log("Rover Action published");
        Debug.Log($"payload published : {payload}");
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
            PublishPayload(payload, publishTopic);
            if (list.actions[0].action == "set_speed") continue;
            yield return new WaitForSeconds(list.actions[0].value);
        }
    }

    private void PublishPayload(string payload, string topic)
    {
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(payload));
        Debug.Log("Rover Action published");
        Debug.Log($"payload published : {payload}");
    }
}
