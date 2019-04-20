using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCModule : CharacterModule
{
    // 생성할 때, 캐릭터를 바로 세팅하는 생성자  함수
    public NPCModule(Character character) : base(character)
    {
    }

    override public void BuildStateList()
    {
        base.BuildStateList();
        _character.GetStateDic().Add(Character.eState.IDLE, new IdleState());

        for (int i = 0; i < _character.GetStateDic().Count; i++)
        {
            Character.eState state = (Character.eState)i;
            _character.GetStateDic()[state].SetCharacter(_character);
        }

        _character.ChangeState(Character.eState.IDLE);
    }
}
