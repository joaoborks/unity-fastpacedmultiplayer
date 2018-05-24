/**
 * AuthoritativeCharacter.cs
 * Created by: Joao Borks
 * Created on: 30/04/17 (dd/mm/yy)
 */

using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

[NetworkSettings(channel = 2)]
public class AuthoritativeCharacter : NetworkBehaviour
{
    public float Speed { get { return speed; } }
    /// <summary>
    /// Controls how many inputs are needed before sending update command
    /// </summary>
    public int InputBufferSize { get; private set; }

    /// <summary>
    /// Controls how many input updates are sent per second
    /// </summary>
    [SerializeField, Range(10, 50), Tooltip("In steps per second")]
    int inputUpdateRate = 10;
    [HideInInspector, SerializeField, Range(5f, 15f)]
    float speed = 6.25f;

    [SyncVar(hook = "OnServerStateChange")]
    public CharacterState state = CharacterState.Zero;
    
    IAuthCharStateHandler stateHandler;
    AuthCharServer server;

    CharacterController charCtrl;

    void Awake()
    {
        InputBufferSize = (int)(1 / Time.fixedDeltaTime) / inputUpdateRate;
    }

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
    public void CmdMove(Vector2[] inputs)
    {
        server.Move(inputs);
    }
}