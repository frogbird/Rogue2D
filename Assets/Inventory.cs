using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<ConsumableItem> shit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void InsertItem(ConsumableItem consumableItem)
    {
        shit.Add(consumableItem);
    }

}
