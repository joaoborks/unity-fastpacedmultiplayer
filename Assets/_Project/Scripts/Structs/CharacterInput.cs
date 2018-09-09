using UnityEngine;

public struct CharacterInput
{
    public CharacterInput(Vector2 _dir, int _inputNum)
    {
        dir = _dir;
        inputNum = _inputNum;
    }
    public Vector2 dir;
    public int inputNum;
}
