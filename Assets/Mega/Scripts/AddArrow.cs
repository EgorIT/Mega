

using UnityEngine;

public class AddArrow : MonoBehaviour {
    public GameObject prefab;
    public void AddArrow3 () {

        var allTra = FindObjectsOfType<Transform>();
        for (int i = 0; i < allTra.Length; i++) {
            if (allTra[i].name == "Arrow_shop_new") {
                var pr = Instantiate(prefab);
                pr.transform.parent = allTra[i];
            }
            
        }
        Debug.Log("EEEEEEEEEEEEEE");
    }
}