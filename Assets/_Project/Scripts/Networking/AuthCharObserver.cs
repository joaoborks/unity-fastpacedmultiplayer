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
    int clientTick = 0;
    int ticksBehind = 10;

    void Awake()
    {
        character = GetComponent<AuthoritativeCharacter>();
        stateBuffer = new LinkedList<CharacterState>();
        SetObservedState(character.state);
        AddState(character.state);
    }

    void Update()
    {
        int pastTick = clientTick - ticksBehind;
        var fromNode = stateBuffer.First;
        var toNode = fromNode.Next;
        while (toNode != null && toNode.Value.timestamp <= pastTick)
        {
            fromNode = toNode;
            toNode = fromNode.Next;
            stateBuffer.RemoveFirst();
        }
        SetObservedState(toNode != null ? CharacterState.Interpolate(fromNode.Value, toNode.Value, pastTick) : fromNode.Value);
    }

    void FixedUpdate()
    {
        clientTick++;
    }

    public void OnStateChange(CharacterState newState)
    {
        clientTick = newState.timestamp;
        AddState(newState);
    }

    void AddState(CharacterState state)
    {
        if (stateBuffer.Count > 0 && stateBuffer.Last.Value.timestamp > state.timestamp)
        {
            return;
        }
        stateBuffer.AddLast(state);
    }

    void SetObservedState(CharacterState newState)
    {
        character.SyncState(newState);
    }
}