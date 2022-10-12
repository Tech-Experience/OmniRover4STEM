using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mqtt Topic", menuName = "ScriptableObject/MqttTopic")]
public class MqttTopic : ScriptableObject
{
    public string topic;
}
