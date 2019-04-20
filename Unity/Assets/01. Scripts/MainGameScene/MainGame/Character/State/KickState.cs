using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickState : State
{
    override public void Start()
    {
        _character.PlayAnimation("kick", () =>
        {
            _character.ChangeState(Character.eState.IDLE);
        });
    }
}
