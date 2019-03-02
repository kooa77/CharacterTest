using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    override public void Start()
    {
        _character.PlayAnimation("idle2", () =>
        {
            _character.ChangeState(Character.eState.KICK);
        });
    }
}
