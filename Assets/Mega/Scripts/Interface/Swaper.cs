using System.Collections;
using System.Collections.Generic;
using Assets.Mega.Scripts;
using UnityEngine;

public class Swaper : MonoBehaviour {

    public Table target;
    public Table current;
    public RectTransform rt;
    
    public void DelayedSwap () {
        StartCoroutine(Delayer());
    }

    private IEnumerator Delayer () {
        yield return new WaitForSeconds(3f);
        Swap();
    }

    public void Swap () {
       
        current.RollOut();
        target.RollIn();
        ButtonAdds.inst.SetActiveButtons(target.gameObject.name == "Basic (Table)" || target.gameObject.name == "Basic");
        if (rt) {
            rt.anchoredPosition = new Vector2(0, 0);
        }
      
    }
}
