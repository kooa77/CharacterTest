using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Character _character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Animation Event

    void AnimationEnd()
    {
        Debug.Log("AnimationEnd");

        switch(_character.GetState())
        {
            case Character.eState.IDLE:
                _character.ChangeState(Character.eState.WAIT);
                break;
            case Character.eState.WAIT:
                _character.ChangeState(Character.eState.KICK);
                break;
            case Character.eState.KICK:
                _character.ChangeState(Character.eState.IDLE);
                break;
        }
    }
}
