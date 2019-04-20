using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : State
{
    override public void Start()
    {
        _character.PlayAnimation("slide", null);
        _character.StartMove(4.5f);
    }
}
