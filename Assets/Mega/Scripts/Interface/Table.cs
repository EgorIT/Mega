using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Table : MonoBehaviour {
    public float seconds;
    public TableData tableData;
    
    private RectTransform rt;
    private Coroutine countdown;
    private Vector3 outPosition = new Vector3(0, -560, 0);
    private Vector3 inPosition = new Vector3(0, 0, 0);

    private bool roll = true;
    // Use this for initialization
    void Start () {
        rt = gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition = outPosition;
    }

    public void RollIn()
    {
        if (roll)
        {
            roll = false;
            StartCoroutine(Rolling(true));
        }
    }

    private IEnumerator Rolling (bool rollIn) {
        float time = 0;
        Vector3 startPosition = rt.anchoredPosition;

        while(time < 0.3f) {
            if(rollIn)
                rt.anchoredPosition = Vector3.Lerp(outPosition, inPosition, time / 0.3f);
            else {
                rt.anchoredPosition = Vector3.Lerp(inPosition, outPosition, time / 0.3f);
            }
            time += Time.deltaTime;
            yield return null;
        }
        if(rollIn) {
            rt.anchoredPosition = inPosition;
            Countdown();
        } else {
            rt.anchoredPosition = outPosition;
        }
        yield return null;
    }

    public void RollOut () {
        if (!roll)
        {
            roll = true;
            StartCoroutine(Rolling(false));
        }
    }

    public void Countdown () {
        if(countdown != null)
            StopCoroutine(countdown);
        countdown = StartCoroutine(CountdownCoroutine(seconds));
    }

    public IEnumerator CountdownCoroutine (float seconds) {
        Debug.Log("start countdown");
        yield return new WaitForSeconds(seconds);
        RollOut();
        yield return null;
    }

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown("a")) {
            RollIn();
        }

        if(Input.GetKeyDown("b")) {
            RollOut();
        }
    }
}
