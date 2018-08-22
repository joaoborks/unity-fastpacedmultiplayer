/**
 * AuthCharPredictor.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class AuthCharPredictor : MonoBehaviour, IAuthCharStateHandler
{
    LinkedList<CharacterInput> pendingInputs;
    AuthoritativeCharacter character;
    CharacterState predictedState;
    private CharacterState lastServerState = CharacterState.Zero;

    void Awake()
    {
        pendingInputs = new LinkedList<CharacterInput>();
        character = GetComponent<AuthoritativeCharacter>();
    }

    public void AddInput(CharacterInput input)
    {
        pendingInputs.AddLast(input);
        ApplyInput(input);    
        character.SyncState(predictedState);
    }

    public void OnStateChange(CharacterState newState)
    {
        if (newState.timestamp <= lastServerState.timestamp) return;
        while (pendingInputs.Count > 0 && pendingInputs.First().inputNum <= newState.moveNum)
        {
            pendingInputs.RemoveFirst();
        }
        predictedState = newState;
        lastServerState = newState;
        UpdatePredictedState();
    }

    void UpdatePredictedState()
    {
        foreach (CharacterInput input in pendingInputs)
        {
            ApplyInput(input);
        }
        character.SyncState(predictedState);
    }

    void ApplyInput(CharacterInput input)
    {
        predictedState = CharacterState.Move(predictedState, input, character.Speed, 0);
    }

}