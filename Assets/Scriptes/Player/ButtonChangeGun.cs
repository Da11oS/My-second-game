using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonChangeGun : MonoBehaviour
{
    public static bool OnClick;
    delegate void Change();

    private GameObject _player;
    private PlayerWeapons _playerWeapons;
    private Change Drop;
    private void Start()
    {
            _playerWeapons = FindObjectOfType<PlayerWeapons>();
            _player = _playerWeapons.gameObject;
            Drop =  _player.GetComponent<PlayerWeapons>().DropWeapon;
            OnClick = false;
    }
    public void OnMouseDown()
    {
        if (_playerWeapons.CurrentWeapon != null)
        {
            Drop();
        }
            _playerWeapons.SetWeaponParameters();
        OnClick = true;
    }

    public void OnMouseUp()
    {
        OnClick = false;
    }
}
