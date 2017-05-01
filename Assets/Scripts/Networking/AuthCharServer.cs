/**
 * AuthCharServer.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;

public class AuthCharServer : MonoBehaviour
{
    Queue<Vector2> inputBuffer;
    AuthoritativeCharacter character;
    int movesMade;
    int serverTick;

    void Awake()
    {
        inputBuffer = new Queue<Vector2>();
        character = GetComponent<AuthoritativeCharacter>();
        character.state = CharacterState.Zero;
    }

    void FixedUpdate()
    {
        serverTick++;
        if (movesMade > 0)
            movesMade--;
        if (movesMade == 0)
        {
            CharacterState state = character.state;
            while ((movesMade < character.inputBufferSize && inputBuffer.Count > 0))
            {
                state = CharacterState.Move(state, inputBuffer.Dequeue(), serverTick);
                movesMade++;
            }
            if (movesMade > 0)
            {
                character.state = state;
                character.OnServerStateChange(state);
            }
        }
    }

    public void Move(Vector2[] inputs)
    {
        foreach (Vector2 input in inputs)
            inputBuffer.Enqueue(input);
    }
}