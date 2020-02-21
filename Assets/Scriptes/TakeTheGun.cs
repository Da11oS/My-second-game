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
    private GameObject takeButton;
    private GameObject gunInstance;
    private void Awake()
    {
        takeButton = GameObject.Find("ButtonDropGun");
        gunInstance = GameObject.Find("GunPosition");
    }
   

    public void DropGun()
    {
        if (takeGun)
        {
            GetComponent<SpriteRenderer>().sprite = defaultPlayerSprite;
            gun = Instantiate(Resources.Load("Pistol", typeof(GameObject)) as GameObject, gunInstance.transform.position, Quaternion.identity);
            gun.GetComponent<CircleCollider2D>().isTrigger = false;
            gun.GetComponent<Rigidbody2D>().AddForce(transform.up * 15, ForceMode2D.Impulse);
            myGun = null;
            takeGun = false;
        }
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
