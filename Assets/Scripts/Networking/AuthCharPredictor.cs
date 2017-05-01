/**
 * AuthCharPredictor.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;

public class AuthCharPredictor : MonoBehaviour, IAuthCharStateHandler
{
    Queue<Vector2> pendingMoves;
    AuthoritativeCharacter character;
    CharacterState predictedState;

    void Awake()
    {
        pendingMoves = new Queue<Vector2>();
        character = GetComponent<AuthoritativeCharacter>();
        UpdatePredictedState();
    }

    public void AddInput(Vector2 input)
    {
        pendingMoves.Enqueue(input);
        UpdatePredictedState();
    }

    public void OnStateChange(CharacterState newState)
    {
        var n = predictedState.moveNum - character.state.moveNum;
        while (n >= 0 && pendingMoves.Count > n)
            pendingMoves.Dequeue();
        UpdatePredictedState();
    }

    void UpdatePredictedState()
    {
        predictedState = character.state;
        foreach (Vector2 input in pendingMoves)
            predictedState = CharacterState.Move(predictedState, input, 0);
        character.SyncState(predictedState);
    }
}