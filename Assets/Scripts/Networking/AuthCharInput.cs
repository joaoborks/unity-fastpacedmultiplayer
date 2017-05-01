/**
 * AuthCharInput.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;

public class AuthCharInput : MonoBehaviour
{
    List<Vector2> inputBuffer;
    AuthoritativeCharacter character;
    AuthCharPredictor predictor;

    void Awake()
    {
        inputBuffer = new List<Vector2>();
        character = GetComponent<AuthoritativeCharacter>();
        predictor = GetComponent<AuthCharPredictor>();
    }

    void FixedUpdate()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputBuffer.Count == 0 && input == Vector2.zero)
            return;
        predictor.AddInput(input);
        inputBuffer.Add(input);
        if (inputBuffer.Count < character.inputBufferSize)
            return;
        character.CmdMove(inputBuffer.ToArray());
        inputBuffer.Clear();
    }
}