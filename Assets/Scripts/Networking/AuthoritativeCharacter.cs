/**
 * AuthoritativeCharacter.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 2)]
public class AuthoritativeCharacter : NetworkBehaviour
{
    public int inputBufferSize = 5;
    public int interpolationDelay = 10;

    [SyncVar(hook = "OnServerStateChange")]
    public CharacterState state = CharacterState.Zero;

    IAuthCharStateHandler stateHandler;
    AuthCharServer server;

    public override void OnStartServer()
    {
        base.OnStartServer();
        server = gameObject.AddComponent<AuthCharServer>();
    }

    void Start()
    {
        if (!isLocalPlayer)
        {
            stateHandler = gameObject.AddComponent<AuthCharObserver>();
            return;
        }
        stateHandler = gameObject.AddComponent<AuthCharPredictor>();
        gameObject.AddComponent<AuthCharInput>();
    }

    public void SyncState(CharacterState overrideState)
    {
        transform.position = overrideState.position;
    }

    public void OnServerStateChange(CharacterState newState)
    {
        state = newState;
        if (stateHandler != null)
            stateHandler.OnStateChange(newState);
    }

    [Command(channel = 0)]
    public void CmdMove(Vector2[] inputs)
    {
        server.Move(inputs);
    }
}