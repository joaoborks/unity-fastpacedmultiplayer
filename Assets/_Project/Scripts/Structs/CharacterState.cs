/**
 * CharacterState.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections;

[System.Serializable]
public struct CharacterState
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public Vector3 velocity;
    public Vector3 angularVelocity;
    public int moveNum;
    public int timestamp;
    
    public override string ToString()
    {
        return string.Format("CharacterState Pos:{0}|Rot:{1}|Vel:{2}|AngVel:{3}|MoveNum:{4}|Timestamp:{5}", position, eulerAngles, velocity, angularVelocity, moveNum, timestamp);
    }

    public static CharacterState Zero
    {
        get
        {
            return new CharacterState {
                position = Vector3.zero,
                eulerAngles = Vector3.zero,
                moveNum = 0,
                timestamp = 0
            };
        }
    }

    public static CharacterState Interpolate(CharacterState from, CharacterState to, int clientTick)
    {
        float t = ((float)(clientTick - from.timestamp)) / (to.timestamp - from.timestamp);
        return new CharacterState
        {
            position = Vector3.Lerp(from.position, to.position, t),
            eulerAngles = Vector3.Lerp(from.eulerAngles, to.eulerAngles, t),
            moveNum = 0,
            timestamp = 0
        };
    }

    public static CharacterState Extrapolate(CharacterState from, int clientTick)
    {
        int t = clientTick - from.timestamp;
        return new CharacterState
        {
            position = from.position + from.velocity * t,
            eulerAngles = from.eulerAngles + from.eulerAngles * t,
            moveNum = from.moveNum,
            timestamp = from.timestamp
        };
    }

    public static CharacterState Move(CharacterState previous, Vector2 input, float speed, int timestamp)
    {
        var state =  new CharacterState
        {
            position = speed * Time.fixedDeltaTime * new Vector3(input.x, 0, input.y) + previous.position,
            eulerAngles = previous.eulerAngles,
            moveNum = previous.moveNum + 1,
            timestamp = timestamp
        };
        var timestepInterval = timestamp - previous.timestamp + 1;
        state.velocity = (state.position - previous.position) / timestepInterval;
        state.angularVelocity = (state.eulerAngles - previous.eulerAngles) / timestepInterval;
        return state;
    }
}