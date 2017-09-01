using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour {
    public List<TimeButton> timeButtons;
    public ChangesInfo changesInfo;
    public RectTransform line;
    private int currentNumber = 0;
   
    
    
    private RectTransform rt;
    private Vector3 outPosition = new Vector3(0, -75.5f, 0);
    private Vector3 inPosition = new Vector3(0, 75.5f, 0);

    public Table shopsList;
    public Table shopTable;
    //private int previousNumber;

    void Awake () {
        rt = gameObject.GetComponent<RectTransform>();
        rt.anchoredPosition = outPosition;
    }

    void OnEnable()
    {
        RollIn();
    }

    public void SwitchTimes (int number) {
        MainLogic.inst.SetQuarter(number);
        //Debug.Log(number);
        StartCoroutine(MoveLine(number));
        //StartCoroutine(SwitchTimesWithDelay(number));
    }

    public IEnumerator MoveLine(int number)
    {
        Vector2 targetPosition = new Vector2(number*180,0);
        
        float time = 0;
        Vector3 startPosition = line.anchoredPosition;

        while(time < 0.3f) {
               line.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, time / 0.3f);
            for (int i = 0; i <= line.anchoredPosition.x/180; i++)
            {
                timeButtons[i].SetPressed();
            }
            for (int i = (int)line.anchoredPosition.x/180; i < 9; i++)
            {
                timeButtons[i].SetNormal();
            }
            time += Time.deltaTime;
            yield return null;
        }
        line.anchoredPosition = targetPosition;//Vector3.Lerp(startPosition, targetPosition, time / 0.3f);
        for (int i = 0; i <= line.anchoredPosition.x/180; i++)
        {
            timeButtons[i].SetPressed();
        }
        for (int i = (int)line.anchoredPosition.x/180+1; i < 9; i++)
        {
            timeButtons[i].SetNormal();
        }
        
        timeButtons[0].SetNormal();
        timeButtons[8].SetNormal();
        if (number>=4)
            changesInfo.SetInfo(number-4);
        shopsList.RollIn();
        yield return null;
    }

    public IEnumerator SwitchTimesWithDelay (int number) {
        if(number > currentNumber) {
            for(int i = currentNumber; i <= number; i++) {
                //Debug.Log(i);
                if(timeButtons[i].year) {
                    //Debug.Log(true);
                    timeButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(24, 73);
                } else {
                    timeButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(12, 37);
                }
                yield return new WaitForSeconds(.01f);
            }
        } else {
            for(int i = currentNumber; i > number; i--) {
                //Debug.Log(i);
                if(timeButtons[i].year) {
                    //Debug.Log(true);
                    timeButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(7, 73);
                } else {
                    timeButtons[i].GetComponent<RectTransform>().sizeDelta = new Vector2(2, 37);
                }
                yield return new WaitForSeconds(.01f);
            }
        }
        currentNumber = number;
        int year = 2017 + currentNumber / 5;
        if (number>=4 && number<=11)
        changesInfo.SetInfo(number-4);
        //text.text = year.ToString();
        yield return null;
    }

    public void RollIn () {
        StartCoroutine(Rolling(true));
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
        } else {
            rt.anchoredPosition = outPosition;
        }
        yield return null;
    }

    public void RollOut () {
        StartCoroutine(Rolling(false));
    }
}
