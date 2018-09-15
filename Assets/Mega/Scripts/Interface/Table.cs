using System.Collections;
using Assets.Mega.Scripts;
using UnityEngine;

public class Table : MonoBehaviour {
    //public float seconds;
    public TableData tableData;

    public RectTransform rt;
    private Coroutine countdown;
    private Vector3 outPosition = new Vector3(0, -560, 0);
    private Vector3 inPosition = new Vector3(0, 0, 0);

    private bool canStartRoll = true;

    public bool isButtonRect;

    public bool isSelectTable;

    public DragablePanel dragablePanel;
    public void Awake () {
        rt = gameObject.GetComponent<RectTransform>();
    }

    public void Start() {
        dragablePanel = GetComponent<DragablePanel>();
        if (!isButtonRect) {
            rt.anchoredPosition = outPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = outPosition;
        }
       
    }

    public void RollOut () {
        if(!canStartRoll) {
            canStartRoll = true;
            if(gameObject.activeInHierarchy) {
                StartCoroutine(Rolling(false));
            }
        }
    }

    public void RollIn () {
        if(canStartRoll) {
            InterfaceController.inst.SwapCurrentTable(this);
            canStartRoll = false;
            StartCoroutine(Rolling(true));
        }
    }

    private IEnumerator Rolling (bool rollIn) {

        float time = 0;
        //Debug.Log(rt.name);
        //Vector3 startPosition = rt.anchoredPosition;

        while(time < 0.3f) {
            if (rollIn) {
                rt.anchoredPosition = Vector3.Lerp(outPosition, inPosition, time / 0.3f);
                InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
            } else {
                rt.anchoredPosition = Vector3.Lerp(inPosition, outPosition, time / 0.3f);
                
            }
            time += Time.deltaTime;
            yield return null;
        }
        if(rollIn) {
            rt.anchoredPosition = inPosition;
            InterfaceController.inst.upperPanelRectTransform.anchoredPosition = rt.anchoredPosition;
            Countdown();
 
        } else {
            rt.anchoredPosition = outPosition;
            
        }
      
        yield return null;
    }



    public void Countdown () {
       /* if(countdown != null)
            StopCoroutine(countdown);
        countdown = StartCoroutine(CountdownCoroutine(seconds));*/
    }

    public IEnumerator CountdownCoroutine (float seconds) {
        //Debug.Log("start countdown");
        yield return new WaitForSeconds(seconds);
        RollOut();
        yield return null;
    }

    public void HardHide() {
        StopAllCoroutines();
        rt.anchoredPosition = outPosition;
        canStartRoll = true;
    }

    void Update () {
        /*if(Input.GetKeyDown("a")) {
            RollIn();
        }

        if(Input.GetKeyDown("b")) {
            RollOut();
        }*/
    }
}
