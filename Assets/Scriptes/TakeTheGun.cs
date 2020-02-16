using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheGun : MonoBehaviour
{
    public  Sprite pistolSprite;// = Resources.Load("Наброски2_2.png") as Sprite;
        
    void Update()
    {
        //var sprite = Resources.Load<Sprite>("Sprites/New Sprite");
        //pistolSprite = sprite; 
        //  pistolSprite = Resources.Load<Sprite>("Наброски2_2");
    }
    int i = 0;
    private void OnTriggerStay2D(Collider2D shit)
    {
        if (shit.gameObject.tag == ("PistolGun"))
        {
            // shit.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(shit.gameObject);
                GetComponent<SpriteRenderer>().sprite = pistolSprite;
            }
        }
    }
}
