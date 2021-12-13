
using System;
using UnityEngine;

[System.Serializable]
public class JumpData : Data
{
    [SerializeField] private float terminalVelocity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float jumpTimeout;
    [SerializeField] private float fallTimeout;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private Ground ground;

    public float VerticalVelocity { get; set; }
    public float JumpTimeoutDelta { get; set; }
    public float FallTimeoutDelta { get; set; }

    public float TerminalVelocity => terminalVelocity;
    public float JumpHeight => jumpHeight;
    public float JumpTimeout => jumpTimeout;
    public float FallTimeout => fallTimeout;
    public float Gravity => gravity;
    public Ground Ground => ground;

    // public float VerticalVelocity;
    // public float TerminalVelocity;
    // public float JumpHeight;
    // public float Gravity;
    // public float JumpTimeout;
    // public float FallTimeout;
    // public float JumpTimeoutDelta;
    // public float FallTimeoutDelta;
}

[Serializable]
public class Ground
{
    [SerializeField] private float groundOffset;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundMask;

    public float GroundOffset => groundOffset;
    public float GroundRadius => groundRadius;
    public LayerMask GroundMask => groundMask;
}

