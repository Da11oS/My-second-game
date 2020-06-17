
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ShootingButton : MonoBehaviour
{
    public bool ButtonClick = false;
    public int ClickNumber = 0;
    public Vector2 BulletDirection;
    public static float ShootTimer;
    public static float ShootGap = 0.5f;
    public GameObject BulletText;
    
    private GameObject _gunInstance;
    private GameObject _player;
    private ChangeGun _playerGun;
    private Text _bulletsNumberText;
    private EdgeCollider2D _meleeAttackZone;
    private Animator _playerAnimator;
    private bool isMeleeAttack;
    private void Awake()
    {
        _gunInstance = GameObject.Find("GunPosition");
        _player = GameObject.Find("Bond");
        _playerGun = _player.GetComponent<ChangeGun>();
        _bulletsNumberText =BulletText. GetComponent<Text>();
        _meleeAttackZone = _gunInstance.GetComponent<EdgeCollider2D>();
        _meleeAttackZone.enabled = false;
        _playerAnimator = _player.GetComponent<Animator>();
    }
    private void Update()
    {
        if (ButtonClick)
        {

            if (ShootTimer <= 0)
            {
                if (_playerGun.TakeGun)
                {
                        
                    if (_playerGun.BulletNumber > 0)
                    {   InstantiateBullet();
                        _playerGun.BulletNumber--;
                        InstallBulletStoreText();
                    }
                    else if(!isMeleeAttack)
                        MeleeAttack();
                }
                else
                {
                    if(!isMeleeAttack)
                    MeleeAttack();
                }
            }
            if (ShootTimer > 0)
                ShootTimer -= Time.deltaTime;
        }
    }
    private void OnMouseUp()
    {
        ButtonClick = false;
    }
    private void OnMouseDown()
    {
        ButtonClick = true;
     
    }

    public void InstantiateBullet()
    {
        
            Instantiate(_playerGun.Bullet, _gunInstance.transform.position, _gunInstance.transform.rotation);
            ShootTimer = 0.5f;
    }
    public void InstallBulletStoreText()
    {
      _bulletsNumberText.text = _playerGun.BulletNumber.ToString() + "/" + _player.GetComponent<ChangeGun>().StoreValue;
    }
    private void MeleeAttack()
    {
        Debug.Log("MeleeAttack");
        _meleeAttackZone.enabled = true;
        _playerAnimator.SetBool("MeleeAttack", true);
        StartCoroutine(MeleeAttackEnd());
    }

    private IEnumerator MeleeAttackEnd()
    {
        float time = 0.5f;
        isMeleeAttack = true;
        for (int i = 0; i <= 1; i++)
        {
            if (i == 1)
            {
                _meleeAttackZone.enabled = false;
                _playerAnimator.SetBool("MeleeAttack", false);
                time = 0.3f;
            }
            
            yield return new WaitForSeconds(time);
        }

        isMeleeAttack = false;

    }
}
