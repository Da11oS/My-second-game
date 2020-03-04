using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TakeTheGun : MonoBehaviour
{
    public  static GameObject MyGun;
    public  Sprite PistolSprite;
    public  bool TakeGun = false;
    public  Sprite DefaultPlayerSprite;
    public  GameObject _gun;
    private GameObject _takeButton;
    private GameObject _gunInstance;
    private UnityEvent<string> _onGunChange;

    private string _typeOfGun;
    
    private void Awake()
    {
        _takeButton = GameObject.Find("ButtonDropGun");
        _gunInstance = GameObject.Find("GunPosition");
       
    }

    public void DropGun()
    {
        if (TakeGun)
        {
            GetComponent<SpriteRenderer>().sprite = DefaultPlayerSprite;
            _gun = Instantiate(Resources.Load("Pistol", typeof(GameObject)) as GameObject, _gunInstance.transform.position, Quaternion.identity);
            _gun.GetComponent<CircleCollider2D>().isTrigger = false;
            _gun.GetComponent<Rigidbody2D>().AddForce(transform.up * 15, ForceMode2D.Impulse);
            MyGun = null;
            TakeGun = false;
            _typeOfGun = null;
        }
    }

    private void OnTriggerStay2D(Collider2D shit)
    {
        if (_takeButton.GetComponent<ButtonDropGun>().PushButtonDropGun)
        {
            if (shit.gameObject.CompareTag("PistolGun"))//(shit.gameObject.tag == ("PistolGun"))
            {
                ChangeGun(shit, "Pistol");
            }
        }
    }
    private void ChangeGun(Collider2D shit,string gunType)
    {
        _typeOfGun = gunType;
        _onGunChange.Invoke(_typeOfGun);
        DropGun();
        MyGun = shit.gameObject;
        Destroy(shit.gameObject);
        GetComponent<SpriteRenderer>().sprite = PistolSprite;
        TakeGun = true;
    }
    private void ChangePlayerSprite()
    {

    }
}
