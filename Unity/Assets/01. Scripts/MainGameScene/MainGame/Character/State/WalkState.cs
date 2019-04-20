using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : State
{
    override public void Start()
    {
        _character.PlayAnimation("walk", null);
        _character.StartMove(1.5f);
    }
}
