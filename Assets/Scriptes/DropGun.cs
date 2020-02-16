using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGun : MonoBehaviour
{
    public Sprite defaultPlayerSprite;
    private GameObject player,gun;
    public GameObject pistol;

    void Start()
    {
            player = GameObject.Find("Bond");
            //var tempPistol = Resources.Load<GameObject>("Prefab/Pistol");
            //pistol = tempPistol;
        //pistol = Resources.Load("Pistol") as GameObject;
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        
        player.GetComponent<SpriteRenderer>().sprite = defaultPlayerSprite;
        gun = Instantiate(pistol, player.transform.position, Quaternion.identity);
        gun.GetComponent<Rigidbody2D>().AddForce(transform.up * 2 * Time.deltaTime, ForceMode2D.Impulse);
    }
}
