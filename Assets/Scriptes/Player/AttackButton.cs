
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class AttackButton : MonoBehaviour
{
    public static bool OnClick = false;
    public float ShootTimer;
    public GameObject BulletText;
    
    private IWeapon _playerWeapon;
    private PlayerWeapons _playerWeapons;

    private bool isMeleeAttack;

    private void Awake()
    {
        _playerWeapons = FindObjectOfType<PlayerWeapons>();

    }
    private void Update()
    {
        if (OnClick)
        {

            if (ShootTimer <= 0)
            {
                if (_playerWeapon != null)
                {
                   
                    _playerWeapon.Attack();
                    _playerWeapons.SetBulletText();
                     ShootTimer = _playerWeapon.GetAttackSpeed();
                }
                else
                {
                    _playerWeapons.SetBulletText(null);
                }
            }
            if (ShootTimer > 0)
                ShootTimer -= Time.deltaTime;
        }
    }
    private void OnMouseUp()
    {
        OnClick = false;
        if (_playerWeapons.CurrentWeapon.GetComponent<Weapon>() != null)
        _playerWeapon = _playerWeapons.CurrentWeapon.GetComponent<Weapon>().WeaponType;
    }
    private void OnMouseDown()
    {
        OnClick = true;

    }


    //private void MeleeAttack()
    //{
    //    Debug.Log("MeleeAttack");
    //    _meleeAttackZone.enabled = true;
    //    _playerAnimator.SetBool("MeleeAttack", true);
    //    StartCoroutine(MeleeAttackEnd());
    //}

    //private IEnumerator MeleeAttackEnd()
    //{
    //    float time = 0.5f;
    //    isMeleeAttack = true;
    //    for (int i = 0; i <= 1; i++)
    //    {
    //        if (i == 1)
    //        {
    //            _meleeAttackZone.enabled = false;
    //            _playerAnimator.SetBool("MeleeAttack", false);
    //            time = 0.3f;
    //        }

    //        yield return new WaitForSeconds(time);
    //    }

    //    isMeleeAttack = false;

    //}
}
