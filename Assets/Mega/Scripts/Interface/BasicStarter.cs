using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStarter : MonoBehaviour
{

	public Table table;

    public void OnEnable() {
	    Debug.Log("OnEnable");
		table.RollIn();
	}

    public void OnDisable() {
        Debug.Log("OnDisable");
        table.RollOut();
    }

}
