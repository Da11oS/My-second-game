using UnityEngine;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public int BulletNumbers;
    [HideInInspector]
    public IWeapon WeaponType;
    [HideInInspector]
    public float AttackSpeed;
    [HideInInspector]
    public int MagValue;
    [HideInInspector]
    public int BulletLeft;
    [HideInInspector]
    public string BulletLeftString;

    public enum Type
    { Pistol, ShotGun}
    [SerializeField]
    private Type _type;
    [SerializeField]
    private GameObject Bullet;

    private void Start()
    {
        switch(_type)
        { 
           case Type.Pistol:
           WeaponType = new PistolGun(Bullet, 2, 6);
           break;
           case Type.ShotGun:
           WeaponType = new ShootGun(Bullet);
           break;
           default:
           WeaponType = new Arm(2);
           break;
        }
    }

}
