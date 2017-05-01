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

    List<Vector2> inputBuffer;
    AuthoritativeCharacter character;
    AuthCharPredictor predictor;

    Vector2 simVector;

    void Awake()
    {
        inputBuffer = new List<Vector2>();
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
        predictor.AddInput(input);
        inputBuffer.Add(input);
        if (inputBuffer.Count < character.inputBufferSize)
            return;
        character.CmdMove(ConvertInputArray());
        inputBuffer.Clear();
    }

    public void Invert()
    {
        simVector.x *= -1;
    }

    CompressedInput[] ConvertInputArray()
    {
        var compressedArray = new CompressedInput[inputBuffer.Count];
        for (int i = 0; i < compressedArray.Length; i++)
            compressedArray[i] = new CompressedInput(inputBuffer[i]);
        return compressedArray;
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