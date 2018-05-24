/**
 * AuthCharObserver.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;

public class AuthCharObserver : MonoBehaviour, IAuthCharStateHandler
{
    LinkedList<CharacterState> stateBuffer;
    AuthoritativeCharacter character;
    CharacterState interpolated;
    int clientTick;

    void Awake()
    {
        character = GetComponent<AuthoritativeCharacter>();
        stateBuffer = new LinkedList<CharacterState>();
        SetObservedState(character.state);
        AddState(character.state);
    }

    void Update()
    {
        var fromNode = stateBuffer.First;
        var toNode = fromNode.Next;
        while (toNode != null && toNode.Value.timestamp <= clientTick)
        {
            fromNode = toNode;
            toNode = fromNode.Next;
            stateBuffer.RemoveFirst();
        }
        SetObservedState(toNode != null ? CharacterState.Interpolate(fromNode.Value, toNode.Value, clientTick) : CharacterState.Extrapolate(fromNode.Value, clientTick));
    }

    void FixedUpdate()
    {
        clientTick++;
    }

    public void OnStateChange(CharacterState newState)
    {
        AddState(newState);
    }

    void AddState(CharacterState state)
    {
        stateBuffer.AddLast(state);
        clientTick = state.timestamp;
        while (stateBuffer.First.Value.timestamp < clientTick)
            stateBuffer.RemoveFirst();
        interpolated.timestamp = Mathf.Max(clientTick, stateBuffer.First.Value.timestamp);
        stateBuffer.AddFirst(interpolated);
        if (interpolated.timestamp < clientTick)
            return;
        interpolated.timestamp = clientTick;
        stateBuffer.AddFirst(interpolated);
    }

    void SetObservedState(CharacterState newState)
    {
        interpolated = newState;
        character.SyncState(newState);
    }
}