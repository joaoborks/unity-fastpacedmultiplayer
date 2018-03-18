/**
 * IAuthCharStateHandler.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

public interface IAuthCharStateHandler
{
    void OnStateChange(CharacterState newState);
}