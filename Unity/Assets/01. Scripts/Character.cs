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
        IdleState();
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 상태

    void IdleState()
    {
        string triggerName = "idle1";
        int randValue = Random.Range(1, 6);
        triggerName = "idle" + randValue;
        _animator.SetTrigger(triggerName);
    }
}
