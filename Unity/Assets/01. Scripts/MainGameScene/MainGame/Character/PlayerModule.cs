using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModule : CharacterModule
{
    // 생성할 때, 캐릭터를 바로 세팅하는 생성자  함수
    public PlayerModule(Character character) : base(character)
    {
    }

    override public void BuildStateList()
    {
        base.BuildStateList();
        _character.GetStateDic().Add(Character.eState.IDLE, new PlayerIdleState());

        for (int i = 0; i < _character.GetStateDic().Count; i++)
        {
            Character.eState state = (Character.eState)i;
            _character.GetStateDic()[state].SetCharacter(_character);
        }

        _character.ChangeState(Character.eState.IDLE);
    }

    override public void UpdateAI()
    {
        base.UpdateAI();

        // Input 처리
        if (true == Input.GetMouseButtonUp(0))   // 유니티에서 마우스 입력 처리 방식
        {
            Vector2 clickPos = Input.mousePosition;

            // 클릭한 화면좌표와 대응되는 월드 좌표 알아내야함.
            // Raycast 사용
            Ray ray = Camera.main.ScreenPointToRay(clickPos);
            RaycastHit hitInfo;
            if (true == Physics.Raycast(ray, out hitInfo, 100.0f, 1 << LayerMask.NameToLayer("Ground")))
            {
                Vector3 destPos = hitInfo.point;
                _character.SetDestination(destPos);
                _character.ChangeState(Character.eState.WALK);
            }
        }
    }
}
