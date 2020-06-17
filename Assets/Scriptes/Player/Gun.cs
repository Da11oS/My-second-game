using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public int BulletNumber;
    public Text BulletsNumberText;

    private ChangeGun _target;
    private void Start()
    {
        _target = GetComponent<ChangeGun>();
    }
    private void Update()
    {
        if(BulletNumber!= _target.BulletNumber)
            SetBulletNumber();
    }

    private void SetBulletNumber()
    {
        BulletNumber = _target.BulletNumber;
    }

}