using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCenter
{
    // Singleton
    static DataCenter _instance = null;
    public static DataCenter GetInstance()
    {
        if(null == _instance)
        {
            _instance = new DataCenter();
        }
        return _instance;
    }

    // 생성자를 외부에서 실행 하지 못하게 바꿈
    private DataCenter() { }


    // CharacterCount

    int _count = 0;

    public void AddCount()
    {
        _count++;
    }

    public int GetCount()
    {
        return _count;
    }
}
