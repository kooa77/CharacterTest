using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointList : MonoBehaviour
{
    void Awake()
    {
        // 모든 웨이포인트 게임 오브젝트를 긁어와서
        // 리스트로 만드는 과정
        List<GameObject> wayPointList = new List<GameObject>();
        int wayPointCount = transform.childCount;
        for (int i = 0; i < wayPointCount; i++)
        {
            GameObject wayPointObject = transform.GetChild(i).gameObject;
            wayPointList.Add(wayPointObject);
        }

        // Waypoint 스크립트에 리스트 세팅
        for (int i = 0; i < wayPointCount; i++)
        {
            GameObject wayPointObject = transform.GetChild(i).gameObject;
            WayPoint wayPointScript = wayPointObject.GetComponent<WayPoint>();
            wayPointScript.SetWaypointList(wayPointList);
        }
    }

	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
