using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Mega.Scripts;
using UnityEngine;

public class InterfaceController : MonoBehaviour {
    public static InterfaceController inst;

    public Table basicTable;
    public Table table;
    public Timeline timeline;
    public ChangesInfo changesInfo;
    public TableData tableData;

    public List<Table> listTables = new List<Table>();
    public RectTransform upperPanelRectTransform;

    public bool isDrag;

    public void Awake () {
        inst = this;
    }

    public void Start() {
        listTables = GameObject.FindObjectsOfType<Table>().ToList();
    }

    //[ContextMenu("HideAllTable")]
    //public void HideAllTable() {
    //    for (int i = 0; i < listTables.Count; i++) {
    //        listTables[i].RollOut();
    //    }
    //}

    [ContextMenu("HardHideAllTable")]
    public void HardHideAllTable () {
        for(int i = 0; i < listTables.Count; i++) {
            listTables[i].HardHide();
        }
    }

    [ContextMenu("ShowBasic")]
    public void ShowBasic() {
        //HardHideAllTable();
        basicTable.RollIn();
    }

    //[ContextMenu("HardHideBasic")]
    //public void HardHideBasic () {
    //    basicTable.HardHide();
    //}

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
    
    public void SetShop (string name) {
        changesInfo.SetShop(name);
    }

    public void GoToAllMega() {
        MainLogic.inst.GoAllMega();
    }
}
