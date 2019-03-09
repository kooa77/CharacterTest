using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitState : State
{
    override public void Start()
    {
        _character.PlayAnimation("wait", () =>
        {
            _character.ChangeState(Character.eState.KICK);
        });
    }
}
