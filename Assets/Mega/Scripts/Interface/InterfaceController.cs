using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;

public class InterfaceController : MonoBehaviour {
    public static InterfaceController instance = null;

    public Table table;
    public Timeline timeline;
    public ChangesInfo changesInfo;
    public TableData tableData;

    public void TableOn () {
        table.RollIn();
    }

    public void TableOff () {
        table.RollOut();
    }

    public void TimelineOn () {
        Debug.Log("tlcall");
        timeline.RollIn();
    }

    public void TimelineOff () {
        timeline.RollOut();
    }

    void Awake () {
        instance = this;
    }

    public void SetShop (string name) {
        changesInfo.SetShop(name);
    }

    public void GoToAllMega() {
        MainLogic.inst.GoAllMega();
    }
}
