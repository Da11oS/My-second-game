using UnityEngine;


public interface  IWeapon
{

     void Attack();
     int GetMagValue();
     float GetAttackSpeed();
     string GetBulletLeftString();
}

public class Rifle : MonoBehaviour, IWeapon
{
    public int NumbersOfBullets { get; private set; }
    private GameObject BulletType { get; }
    private Vector2 BulletOriginPosition { get; }
    public int MagValue { get; }
    public float AttackSpeed { get; protected set; }
    public Rifle(GameObject bullettype, float ShootingSpeed, int numbersOfBulletsMax)
    {

    }
    public void Attack()
    {
        if (NumbersOfBullets > 0)
        {
            NumbersOfBullets--;
            Instantiate(BulletType, BulletOriginPosition, Quaternion.identity);
        }
    }
    public int GetLeftBullets()
    {
        return NumbersOfBullets;
    }
    public int GetMagValue()
    {
        return MagValue;
    }
    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }
    public string GetBulletLeftString()
    {
        return NumbersOfBullets + "/" + MagValue;
    }

}

public class PistolGun : Rifle
{
    public PistolGun(GameObject bullettype, float ShootingSpeed = 2f, int numbersOfBulletsMax = 6) : base(bullettype, ShootingSpeed, numbersOfBulletsMax)
    {

    }
}
public class ShootGun : Rifle
{
    public ShootGun(GameObject bullettype, float ShootingSpeed = 2f, int numbersOfBulletsMax = 6) : base(bullettype, ShootingSpeed, numbersOfBulletsMax)
    {

    }
}
public class Melee : MonoBehaviour, IWeapon
{
    public float AttackSpeed { get; protected set; }
    public Melee(int attackSpeed):base()
    {
        AttackSpeed = attackSpeed;
    }
    public  void Attack()
    {
    }
    public int GetLeftBullets()
    {
        return 0;
    }
    public int GetMagValue()
    {
        return 0;
    }
    public float GetAttackSpeed()
    {
        return AttackSpeed;
    }

    public string GetBulletLeftString()
    {
        return null;
    }
} 

public class Arm : Melee
{
    public Arm(int attackSpeed) : base(attackSpeed)
    {

    }
    

}