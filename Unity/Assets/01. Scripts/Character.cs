using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    // CharacterController를 _animator 에 세팅
    [SerializeField] AnimationController _animationController;

    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _stateDic.Add(eState.IDLE, new IdleState());
        _stateDic.Add(eState.WAIT, new WaitState());
        _stateDic.Add(eState.KICK, new KickState());
        _stateDic.Add(eState.WALK, new WalkState());

        for (int i=0; i< _stateDic.Count; i++)
        {
            eState state = (eState)i;
            _stateDic[state].SetCharacter(this);
        }

        ChangeState(eState.WALK);
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdateState();
        UpdateMove();
    }

    // 상태

    public enum eState
    {
        IDLE,
        WAIT,
        KICK,
        WALK,
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


    // Movement

    CharacterController _characterController;
    float _moveSpeed = 0.0f;

    void UpdateMove()
    {
        Vector3 moveDirection = Vector3.forward;
        Vector3 moveVelocity = moveDirection * _moveSpeed;
        Vector3 gravityVelocity = Vector3.down * 9.8f;  // 중력

        Vector3 finalVelocty = (moveVelocity + gravityVelocity) * Time.deltaTime;
        _characterController.Move(finalVelocty);
    }

    public void StartWalk(float speed)
    {
        _moveSpeed = speed;
    }
}
