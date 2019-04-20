using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    override public void Start()
    {
        _character.PlayAnimation("run", null);
        _character.StartMove(4.5f);
    }

    override public void Update()
    {
        float distance = _character.GetDistanceToTarget();
        if(4.0f < distance && distance <= 5.0f)
        {
            _character.ChangeState(Character.eState.SLIDE);
        }
    }
}
