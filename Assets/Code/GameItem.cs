using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour {

    public Sprite itemSprite;

    public ConsumableItem consumableItem;

    //public PolygonCollider2D pc2;

    //public Rigidbody2D gameBody;

    // Use this for initialization
    void Start () {

        
        itemSprite = consumableItem.itemSprite;
        GetComponent<SpriteRenderer>().sprite = itemSprite;

        gameObject.AddComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        Destroy(gameObject);
    }


}
