/**
 * AuthoritativeCharacter.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using UnityEngine.Networking;

[NetworkSettings(channel = 2, sendInterval = .05f)]
public class AuthoritativeCharacter : NetworkBehaviour
{
    public int inputBufferSize = 5;
    public int interpolationDelay = 10;

    [SyncVar(hook = "OnServerStateChange")]
    public CharacterState state = CharacterState.Zero;
    
    IAuthCharStateHandler stateHandler;
    AuthCharServer server;

    CharacterController charCtrl;

    public override void OnStartServer()
    {
        base.OnStartServer();
        server = gameObject.AddComponent<AuthCharServer>();
    }

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();
        if (!isLocalPlayer)
        {
            stateHandler = gameObject.AddComponent<AuthCharObserver>();
            return;
        }
        GetComponentInChildren<Renderer>().material.color = Color.green;
        stateHandler = gameObject.AddComponent<AuthCharPredictor>();
        gameObject.AddComponent<AuthCharInput>();
    }

    public void SyncState(CharacterState overrideState)
    {
        charCtrl.Move(overrideState.position - transform.position);
    }

    public void OnServerStateChange(CharacterState newState)
    {
        state = newState;
        if (stateHandler != null)
            stateHandler.OnStateChange(state);
    }

    [Command(channel = 0)]
    public void CmdMove(CompressedInput[] inputs)
    {
        server.Move(inputs);
    }
}