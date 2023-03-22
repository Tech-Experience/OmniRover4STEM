#!/usr/bin/python3 -W

# Command the robots to dance in unison

import paho.mqtt.client as mqtt
import paho.mqtt.publish as publish

actions = '{"actions":[{"action":"move_forward","value":1.0},{"action":"move_right","value":1.0},{"action":"move_backward","value":1.0},{"action":"set_speed","value":2.0}]}'

def danceRoutine(actions):
    msgs = []
    for i in range(12):
        msgs.append((f"RoverAction/rover%d" % i, actions, 0, False))
    publish.multiple(msgs, hostname="test.mosquitto.org")

# The callback for when the client receives a CONNACK response from the server.
def on_connect(client, userdata, flags, rc):
    print("Connected with result code "+str(rc))

    # Subscribing in on_connect() means that if we lose the connection and
    # reconnect then subscriptions will be renewed.
    client.subscribe("stemdance")

# The callback for when a PUBLISH message is received from the server.
def on_message(client, userdata, msg):
    global actions
    print(msg.topic+" "+str(msg.payload))
    danceRoutine(actions)

client = mqtt.Client()
client.on_connect = on_connect
client.on_message = on_message

#client.connect("mqtt.eclipseprojects.io", 1883, 60)
client.connect("test.mosquitto.org", 1883, 60)

client.loop_forever()
