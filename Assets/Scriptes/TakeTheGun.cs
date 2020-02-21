using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeTheGun : MonoBehaviour
{
    public  static GameObject myGun;
    public  Sprite pistolSprite;
    public  bool onGun;
    public  bool takeGun = false;
    public  Sprite defaultPlayerSprite;
    public  GameObject gun;
    private GameObject pistol;
    private GameObject takeButton;
    private GameObject playerLegs;
    private GameObject GunPosition;

    private void Awake()
    {
        takeButton = GameObject.Find("ButtonDropGun");
        pistol = Resources.Load("Pistol") as GameObject;
        playerLegs = GameObject.Find("Legs");
        GunPosition = GameObject.Find("GunPosition");
    }
    int i = 0;

    public void DropGun()
    {
        if (takeGun)
        {
            GetComponent<SpriteRenderer>().sprite = defaultPlayerSprite;
            gun = Instantiate(Resources.Load("Pistol", typeof(GameObject)) as GameObject, GetGunStartPosition(), Quaternion.identity);
            gun.GetComponent<CircleCollider2D>().isTrigger = false;
            gun.GetComponent<Rigidbody2D>().AddForce(transform.up * 15, ForceMode2D.Impulse);
            myGun = null;
            takeGun = false;
        }
    }
    private Vector2 GetGunStartPosition()
    {
        float radius = 1.5f;
        float y = radius*radius / (Mathf.Sin(transform.rotation.z) + 1);
        Debug.Log(Mathf.Sin(transform.rotation.z)+", "+ (transform.rotation.z));
        float x = radius * radius - y;
        Debug.Log(new Vector2(transform.position.x + x, transform.position.y + y) +", "+transform.position);
        //return new Vector2(playerLegs.transform.position.x+x, playerLegs.transform.position.y+y); 
        return GunPosition.transform.position;
    }
    private void StatitcObject(GameObject go)
    {
        if(go.GetComponent<Rigidbody2D>()!=null)
        go.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (go.GetComponent<CircleCollider2D>() != null)
            go.GetComponent<CircleCollider2D>().isTrigger = true;
    }
    public void TakeGun( GameObject GUN)
    {
        Debug.Log("Take1");
        if(takeGun)
        DropGun();
            
            
    }

    private void OnTriggerStay2D(Collider2D shit)
    {
        if (shit.gameObject.CompareTag("PistolGun"))//(shit.gameObject.tag == ("PistolGun"))
        {
           
            if (takeButton.GetComponent<ButtonDropGun>().pushButtonDropGun)
            {
              
                    DropGun();
                myGun = shit.gameObject;
                Destroy(shit.gameObject);
                GetComponent<SpriteRenderer>().sprite = GetComponent<TakeTheGun>().pistolSprite;
                takeGun = true;
            }
        }

    }

}
