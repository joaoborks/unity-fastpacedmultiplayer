/**
 * AuthCharInput.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using System.Collections.Generic;

public class AuthCharInput : MonoBehaviour
{
    public static bool simulated = false;

    List<CharacterInput> inputBuffer;
    AuthoritativeCharacter character;
    AuthCharPredictor predictor;
    int currentInput = 0;
    Vector2 simVector;

    void Awake()
    {
        inputBuffer = new List<CharacterInput>();
        character = GetComponent<AuthoritativeCharacter>();
        predictor = GetComponent<AuthCharPredictor>();
        if (simulated)
        {
            simVector.x = Random.Range(0, 1) > 0 ? 1 : -1;
            simVector.y = Random.Range(-1f, 1f);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            simulated = !simulated;
        Vector2 input = simulated ? SimulatedVector() : new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (inputBuffer.Count == 0 && input == Vector2.zero)
            return;
        CharacterInput charInput = new CharacterInput(input, currentInput++);
        predictor.AddInput(charInput);
        inputBuffer.Add(charInput);
    }

    void FixedUpdate()
    {
        if (inputBuffer.Count < character.InputBufferSize)
            return;
        character.CmdMove(inputBuffer.ToArray());
        inputBuffer.Clear();
    }


    Vector2 SimulatedVector()
    {
        if (transform.position.x > 5)
            simVector.x = Random.Range(-1f, 0);
        else if (transform.position.x < -5)
            simVector.x = Random.Range(0, 1f);
        if (transform.position.z > 2 || transform.position.z < -2)
            simVector.y = 0;
        return simVector;
    }
}