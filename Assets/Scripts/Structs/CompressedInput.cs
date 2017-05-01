/**
 * CompressedInput.cs
 * Created by: Joao Borks
 * Created on: 01/05/17 (dd/mm/yy)
 */

using UnityEngine;

[System.Serializable]
public struct CompressedInput
{
    public sbyte x;
    public sbyte y;

    public CompressedInput(Vector2 inputVector)
    {
        x = (sbyte)(inputVector.x * sbyte.MaxValue);
        y = (sbyte)(inputVector.y * sbyte.MaxValue);
    }

    public Vector2 ToVector2
    {
        get
        {
            return new Vector2((float)x / sbyte.MaxValue, (float)y / sbyte.MaxValue);
        }
    }
}