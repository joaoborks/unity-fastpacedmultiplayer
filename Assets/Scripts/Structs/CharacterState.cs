/**
 * CharacterState.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections;
using System.Linq;

public struct CharacterState
{
    public Vector3 position;
    public Vector3 eulerAngles;
    public int moveNum;
    public int timestamp;

    public CharacterState(BitArray syncState)
    {
        position = Vector3.zero;
        eulerAngles = Vector3.zero;
        moveNum = 0;
        timestamp = 0;
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

    public static CharacterState Move(CharacterState previous, Vector2 input, int timestamp)
    {
        return new CharacterState
        {
            position = 0.125f * new Vector3(input.x, 0, input.y) + previous.position,
            eulerAngles = Vector3.zero,
            moveNum = previous.moveNum + 1,
            timestamp = timestamp
        };
    }
}