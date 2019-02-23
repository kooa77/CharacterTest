using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // CharacterController를 _animator 에 세팅
    [SerializeField] AnimationController _animationController;

    // Start is called before the first frame update
    void Start()
    {
        /*
        _animationController.AddEndEvent(()=>
        {
            Debug.Log("Animation Test");
            // Idle -> Wait
            // Wait -> Kick
            // Kick -> Idle
        });
        */
        ChangeState(eState.IDLE);
    }


    // Update is called once per frame
    void Update()
    {
    }

    // 상태

    enum eState
    {
        IDLE,
        WAIT,
        KICK,
    }
    
    void ChangeState(eState state)
    {
        switch (state)
        {
            case eState.IDLE:
                IdleState();
                break;
            case eState.WAIT:
                WaitState();
                break;
            case eState.KICK:
                KickState();
                break;
        }
    }

    void IdleState()
    {
        //_animator.SetTrigger("idle1");
        _animationController.Play("idle1", () =>
        {
            WaitState();
        });
    }

    void WaitState()
    {
        //_animator.SetTrigger("idle2");
        _animationController.Play("idle2", () =>
        {
            KickState();
        });
    }

    void KickState()
    {
        //_animator.SetTrigger("idle5");
        _animationController.Play("idle5", () =>
        {
            IdleState();
        });
    }
}
