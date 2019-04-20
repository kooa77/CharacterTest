using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameCanvas : MonoBehaviour
{
    public Text CountText;

    DataCenter dc;

    void Start ()
    {
    }

	void Update ()
    {
        int curCount = dc.GetCount();
        CountText.text = curCount.ToString();
    }
}
