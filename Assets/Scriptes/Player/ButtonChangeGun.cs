using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonChangeGun : MonoBehaviour
{
    public Sprite DefaultPlayerSprite;
    public GameObject Pistol;
    public UnityEvent  Drop;
    public bool PushButtonDropGun;

    private GameObject _player;
    private GameObject _gun;
    private void Start()
    {
            _player = GameObject.Find("Bond");
    }
    public void OnMouseDown()
    {
        if (_player.GetComponent<ChangeGun>().TakeGun)
        {
            Drop.Invoke();
            _player.GetComponent<ChangeGun>().TakeGun = false;
        }
        PushButtonDropGun = true;
    }
    public void OnMouseUp()
    {
        PushButtonDropGun = false;
    }
}
