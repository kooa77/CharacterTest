﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] string _modelName;
    AnimationController _animationController;
    [SerializeField] List<GameObject> _wayPointList;

    [SerializeField] bool _isPlayer = false;

    int _meetCount = 0;

    void Awake()
    {
        _characterController = gameObject.GetComponent<CharacterController>();

        // 자동화
        {
            // 1.
            {
                // 에디터에서 프리팹을 세팅
                // 세팅한 프리팹을 객체로 생성
                //Transform skinTransfrom;
                //skinTransfrom.SetParent(transform);
            }
            // 2.
            {
                // 프리팹을 로딩한다. (캐릭터 이름 : BoxUnityChan)
                //Transform skinTransfrom;
                //skinTransfrom.SetParent(transform);
            }

            if ( 0 < transform.childCount)
            {
                Transform childTransform = transform.GetChild(0);
                childTransform.gameObject.AddComponent<AnimationController>();
                _animationController = childTransform.gameObject.GetComponent<AnimationController>();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(true == _isPlayer)
        {
            _stateDic.Add(eState.IDLE, new PlayerIdleState());
        }
        else
        {
            _stateDic.Add(eState.IDLE, new IdleState());
        }
        
        _stateDic.Add(eState.WAIT, new WaitState());
        _stateDic.Add(eState.KICK, new KickState());
        _stateDic.Add(eState.WALK, new WalkState());
        _stateDic.Add(eState.RUN, new RunState());
        _stateDic.Add(eState.SLIDE, new SlideState());
        _stateDic.Add(eState.PATROL, new PatrolState());
        _stateDic.Add(eState.DEATH, new DeathState());

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
        if(eState.DEATH != _stateType)
        {
            // Input 처리
            if(true == _isPlayer)
            {
                if(true == Input.GetMouseButtonUp(0))   // 유니티에서 마우스 입력 처리 방식
                {
                    Vector2 clickPos = Input.mousePosition;

                    // 클릭한 화면좌표와 대응되는 월드 좌표 알아내야함.
                    // Raycast 사용
                    Ray ray = Camera.main.ScreenPointToRay(clickPos);
                    RaycastHit hitInfo;
                    if(true == Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
                    {
                        Vector3 destPos = hitInfo.point;
                        SetDestination(destPos);
                        ChangeState(Character.eState.WALK);
                    }
                }
            }

            UpdateState();
            UpdateMove();
            UpdateDeath();
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(gameObject))
            return;

        _meetCount++;
        if(10 < _meetCount)
        {
            ChangeState(eState.DEATH);
            return;
        }

        _lifeTime = 0.0f;

        switch (_stateType)
        {
            case eState.WALK:
            case eState.RUN:
            case eState.SLIDE:
                ChangeState(eState.IDLE);
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 나일 때는 패스
        if (other.gameObject.Equals(gameObject))
            return;

        //_meetCount--;
    }


    // 상태

    public enum eState
    {
        IDLE,
        WAIT,
        KICK,
        WALK,
        RUN,
        SLIDE,
        PATROL,
        DEATH,
    }

    eState _stateType = eState.IDLE;

    public void ChangeState(eState state)
    {
        _stateType = state;
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
    Vector3 _destPoint;

    void UpdateMove()
    {
        Vector3 moveDirection = GetMoveDirection();
        Vector3 moveVelocity = moveDirection * _moveSpeed;
        Vector3 gravityVelocity = Vector3.down * 9.8f;  // 중력

        Vector3 finalVelocty = (moveVelocity + gravityVelocity) * Time.deltaTime;
        _characterController.Move(finalVelocty);

        // 현재 위치와 목적지 까지의 거리를 계산해서
        // 적절한 범위 내에 들어오면 스톱.
        if(0.0f < _moveSpeed)
        {
            float distance = GetDistanceToTarget();
            if (distance < 0.5f)
            {
                _moveSpeed = 0.0f;
                ChangeState(eState.IDLE);
            }
        }
    }

    public float GetDistanceToTarget()
    {
        Vector3 charPos = transform.position;
        Vector3 curPos = new Vector3(charPos.x, 0.0f, charPos.z);
        Vector3 destPos = new Vector3(_destPoint.x, 0.0f, _destPoint.z);
        float distance = Vector3.Distance(curPos, destPos);
        return distance;
    }

    public void StartMove(float speed)
    {
        _moveSpeed = speed;
    }

    public void StopMove()
    {
        _moveSpeed = 0.0f;
    }

    public Vector3 GetWayPoint(int index)
    {
        return _wayPointList[index].transform.position;
    }

    public Vector3 GetRandomWayPoint()
    {
        int index = Random.Range(0, _wayPointList.Count);
        return GetWayPoint(index);
    }

    public int GetWayPointCount()
    {
        return _wayPointList.Count;
    }

    public void SetWaypointList(List<GameObject> wayPointList)
    {
        _wayPointList = wayPointList;
    }

    public void SetDestination(Vector3 destPoint)
    {
        _destPoint = destPoint;
    }

    Vector3 GetMoveDirection()
    {
        // (목적위치 - 현재 위치) 노멀라이즈
        Vector3 charPos = transform.position;
        Vector3 curPos = new Vector3(charPos.x, 0.0f, charPos.z);
        Vector3 destPos = new Vector3(_destPoint.x, 0.0f, _destPoint.z);
        Vector3 direction = (destPos - curPos).normalized;

        Vector3 lookPos = new Vector3(_destPoint.x, charPos.y, _destPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(
                                    transform.rotation,
                                    targetRotation,
                                    360.0f * Time.deltaTime);
        
        return direction;
    }


    // Death

    float _deathTime = 20.0f;
    float _lifeTime = 0.0f;

    void UpdateDeath()
    {
        if(_deathTime <= _lifeTime)
        {
            ChangeState(eState.DEATH);
        }
        _lifeTime += Time.deltaTime;
    }
}
