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
        _stateDic.Add(eState.IDLE, new IdleState());
        _stateDic.Add(eState.WAIT, new WaitState());
        _stateDic.Add(eState.KICK, new KickState());
        
        for (int i=0; i< _stateDic.Count; i++)
        {
            eState state = (eState)i;
            _stateDic[state].SetCharacter(this);
        }

        ChangeState(eState.IDLE);
    }


    // Update is called once per frame
    void Update()
    {
        UpdateState();
    }

    // 상태

    public enum eState
    {
        IDLE,
        WAIT,
        KICK,
    }
    
    public void ChangeState(eState state)
    {
        _state = _stateDic[state];
        _state.Start();
    }

    void UpdateState()
    {
        _state.Update();
    }


    // State

    Dictionary<eState, State> _stateDic = new Dictionary<eState, State>();
    State _state = null;


    // Animation

    public void PlayAnimation(string trigger, System.Action endCallback)
    {
        _animationController.Play(trigger, endCallback);
    }
}
