using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // CharacterController를 _animator 에 세팅
    [SerializeField] Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(eState.IDLE);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 상태

    public enum eState
    {
        IDLE,
        WAIT,
        KICK,
    }
    eState _state = eState.IDLE;

    public void ChangeState(eState state)
    {
        _state = state;
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

    public eState GetState()
    {
        return _state;
    }

    void IdleState()
    {
        _animator.SetTrigger("idle1");
    }

    void WaitState()
    {
        _animator.SetTrigger("idle2");
    }

    void KickState()
    {
        _animator.SetTrigger("idle5");
    }
}
