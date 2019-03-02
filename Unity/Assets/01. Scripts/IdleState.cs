﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    override public void Start()
    {
        _character.PlayAnimation("idle1", () =>
        {
            _character.ChangeState(Character.eState.WAIT);
        });
    }
}