using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    override public void Start()
    {
        _character.StopMove();
        _character.PlayAnimation("idle", () =>
        {
            _character.ChangeState(Character.eState.PATROL);
        });
    }
}
