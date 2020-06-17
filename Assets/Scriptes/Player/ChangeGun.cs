using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeGun : MonoBehaviour
{

    public bool TakeGun = false;
    public int StoreValue;
    public int BulletNumber;
    public Sprite[] PlayerSprites;
    public GameObject[] BulletArray;
    public GameObject Bullet;
    public GameObject BulletsNumberText;
    public GameObject TempGun;
    public UnityEvent OnChangeBulletText;

    private bool _isGun;
    private int _spriteTypeId;
    private int _bulletTypeId;
    private SpriteRenderer _playerSpriteRenderer;
    private GameObject _takeButton;
    private GameObject _gunInstance;
    private DropingGun _dropGun;
    private enum WeaponType { arm, pistol,shotgun };
    private WeaponType _weaponType;
    private WeaponType _handWeapon;


    private void Start()
    {
        _takeButton = GameObject.Find("ButtonDropGun");
        _gunInstance = GameObject.Find("GunPosition");
        BulletsNumberText = GameObject.Find("NumberOfBulletsText");
        _playerSpriteRenderer = GetComponent<SpriteRenderer>();
        _weaponType = WeaponType.arm;
        _handWeapon = _weaponType;
    }
    private void Update()
    {
        if(_weaponType !=_handWeapon)
        {
           ChangeWeapon(_handWeapon);
        }
    }
    private void ChangeWeapon(WeaponType typeOfweapon)
    {
        _weaponType = typeOfweapon;
        switch (typeOfweapon)
        {
            case WeaponType.pistol:
                int pistolStoreValue = 15;
                _isGun = true;
                _spriteTypeId = 1;
                SetWeaponsRecord(0.3f, pistolStoreValue, PlayerSprites[_spriteTypeId]);
                SetBullet(pistolStoreValue, 0);
                Debug.Log("Pistol");
                break;
            case WeaponType.shotgun:
                int shotGunStoreValue = 6;
                _isGun = true;
                _spriteTypeId = 2;
                SetWeaponsRecord(1f, shotGunStoreValue, PlayerSprites[_spriteTypeId]);
                SetBullet(shotGunStoreValue, 1);
                Debug.Log("Shotgun");
                break;
            default:
                Debug.Log("Melee");
                _spriteTypeId = 0;
                _playerSpriteRenderer.sprite = PlayerSprites[_spriteTypeId];
                _isGun = false;
                break;
        }
        if (_isGun)
            OnChangeBulletText.Invoke();
    }
    private void SetBullet(int bulletNumber,int bulletID)
    {
        if (_dropGun.TakeGunCount <= 0)
        {
            _dropGun.TakeGunCount++;
            _dropGun.BulletCount = bulletNumber;//GetComponent<ChangeGun>().GunStoreValue;
        }
        _bulletTypeId = bulletID;
        Bullet = BulletArray[_bulletTypeId];
        BulletNumber = _dropGun.BulletCount;
    }
    private void SetWeaponsRecord(float shootingGap, int storeValue, Sprite weaponSprite)
    {
        _playerSpriteRenderer.sprite = weaponSprite;
        StoreValue = storeValue;
        ShootingButton.ShootGap = shootingGap;
        _isGun = true;
    } 
    private void OnTriggerStay2D(Collider2D shit)
    {
        if (_takeButton.GetComponent<ButtonChangeGun>().PushButtonDropGun)
        {
            if (shit.gameObject.CompareTag("PistolGun"))
            {
                ChangingGun(shit, WeaponType.pistol);
            }
            else if(shit.gameObject.CompareTag("ShotGun"))
            {
                ChangingGun(shit, WeaponType.shotgun);
            }
        }
    }

    private void ChangingGun(Collider2D shit, WeaponType handWeapon)
    {
        if(TakeGun)
         DropGun();
        TempGun = shit.gameObject;
        if (TempGun != null)
        {
            _dropGun = TempGun.GetComponent<DropingGun>();
        }
        _handWeapon = handWeapon;
        TempGun.transform.parent = gameObject.transform;
        TempGun.SetActive(false);
        TakeGun = true;
    }
    public void DropGun()
    {
        TempGun.transform.parent = null;
       
        TempGun.SetActive(true);
        TempGun.gameObject.layer = 11;
        StartCoroutine(ChangeGunLayer());
        TempGun.transform.position = transform.position;
        TempGun.GetComponent<CircleCollider2D>().isTrigger = false;
        _dropGun.BulletCount = BulletNumber;
        TempGun.transform.rotation = _gunInstance.transform.rotation;
        TempGun.GetComponent<Rigidbody2D>().AddForce(transform.up * 15, ForceMode2D.Impulse);
        TakeGun = false;
        _handWeapon = WeaponType.arm;
        BulletsNumberText.GetComponent<Text>().text = "";
         Debug.Log("Пушка брошена?");
    }
    private IEnumerator ChangeGunLayer()
    {
        for (int i = 0; i <= 1; i++)
        {
            if (i == 1)
                TempGun.gameObject.layer = 8;
            yield return new WaitForSeconds(0.5f);
        }

    }


}
