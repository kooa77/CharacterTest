using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModule
{
    protected Character _character;

    public CharacterModule(Character character)
    {
        _character = character;
    }

    virtual public void BuildStateList()
    {
        _character.GetStateDic().Add(Character.eState.WAIT, new WaitState());
        _character.GetStateDic().Add(Character.eState.KICK, new KickState());
        _character.GetStateDic().Add(Character.eState.WALK, new WalkState());
        _character.GetStateDic().Add(Character.eState.RUN, new RunState());
        _character.GetStateDic().Add(Character.eState.SLIDE, new SlideState());
        _character.GetStateDic().Add(Character.eState.PATROL, new PatrolState());
        _character.GetStateDic().Add(Character.eState.DEATH, new DeathState());
    }

    virtual public void UpdateAI()
    {
    }
}
