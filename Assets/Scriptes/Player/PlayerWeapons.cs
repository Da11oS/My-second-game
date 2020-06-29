using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapons : MonoBehaviour
{
    public enum WeaponTypes
    {
        Arm, Pistol, ShotGun
    }

    public WeaponTypes Weapon;
    public GameObject CurrentWeapon;
    [SerializeField]
    [HideInInspector]
    public Text BulletsNumberText;

    private int _impulseForce;
    [SerializeField]
    private GameObject _bulletText;
    private void Start()
    {
        Weapon = WeaponTypes.Arm;
        BulletsNumberText = _bulletText.GetComponent<Text>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<Weapon>() != null)
        {
            System.Type weaponType = collision.gameObject.GetComponent<Weapon>().WeaponType.GetType();

            if (ButtonChangeGun.OnClick)
            {
                if (weaponType == typeof(PistolGun))
                {
                    ChangeWeapon(WeaponTypes.Pistol, collision.gameObject);
                }
                else if (weaponType == typeof(ShootGun))
                {
                    ChangeWeapon(WeaponTypes.ShotGun, collision.gameObject);
                }
            }


        }
    }

    public void SetWeaponParameters()
    {

            Weapon currentWeaponParameters = CurrentWeapon.GetComponent<Weapon>();
            currentWeaponParameters.MagValue = currentWeaponParameters.WeaponType.GetMagValue();
            currentWeaponParameters.AttackSpeed = currentWeaponParameters.WeaponType.GetAttackSpeed();
            SetBulletText();
    }

    public void SetBulletText()
    {
        Weapon currentWeaponParameters = CurrentWeapon.GetComponent<Weapon>();
        BulletsNumberText.text = currentWeaponParameters.WeaponType.GetBulletLeftString();
    }
    public void SetBulletText(string text)
    {
        BulletsNumberText.text = text;
    }
    private void ChangeWeapon(WeaponTypes weapon, GameObject playerWeapon)
    {
        Weapon = weapon;
        CurrentWeapon = playerWeapon;
        if(playerWeapon != null)
        ChangeVisibility(gameObject.transform, false);
    }
    public void DropWeapon()
    {
        CurrentWeapon.GetComponent<Rigidbody2D>().AddForce(transform.up * _impulseForce, ForceMode2D.Impulse);
        ChangeVisibility(null, true);
        ChangeWeapon(WeaponTypes.Arm, null);
    }
    private void ChangeVisibility(Transform parent, bool isVisible)
    {
        CurrentWeapon.transform.parent = parent;
        CurrentWeapon.transform.position = parent.position;
        CurrentWeapon.GetComponent<Renderer>().enabled = isVisible;
        CurrentWeapon.GetComponent<CircleCollider2D>().enabled = isVisible;
        CurrentWeapon.GetComponent<DropingGun>().enabled = isVisible;
    }

}
