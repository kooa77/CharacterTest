using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    Vector3 _prevWayPoint = Vector3.zero;

    override public void Start()
    {
        _prevWayPoint = _character.transform.position;
        _prevWayPoint.y = 0.0f;
        Vector3 wayPoint = _character.GetRandomWayPoint();

        if (wayPoint.Equals(_prevWayPoint))
        {
            _character.ChangeState(Character.eState.WAIT);
        }
        else
        {
            _character.SetDestination(wayPoint);

            int randValue = Random.Range(0, 100);
            if(randValue < 50)
            {
                _character.ChangeState(Character.eState.RUN);
            }
            else
            {
                _character.ChangeState(Character.eState.WALK);
            }

            _prevWayPoint = wayPoint;
        }
        
    }
}
