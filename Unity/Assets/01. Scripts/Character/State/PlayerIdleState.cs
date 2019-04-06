using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    override public void Start()
    {
        _character.StopMove();
        _character.PlayAnimation("idle", null);
    }
}
