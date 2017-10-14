using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
//    private vars
//health
//speed
//ammo
//weapon

//public functions
//health
//    { get; set; }
//    speed {get; set;}
//ammo {get; set;}
//weapon {get; set;}
//move(moveDir)
//fireWeapon()
//repair()



    private float speed = 1.0f;
    private int health = 5;
    private int ammo = 100;
    private int weapon;

    public struct WeaponSystem
    {
        public int weapon, ammo;

        public WeaponSystem(int x1, int x2)
        {
            weapon = x1;
            ammo = x2;

        }
    }

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public int Ammo
    {
        get { return ammo; }
        set { ammo = value; }
    }

    public int Weapon
    {
        get { return weapon; }
        set { weapon = value; }
    }
    public void move(Vector3 moveAmt)
	{
		transform.position += moveAmt * speed * Time.deltaTime;
	}

    private void FireWeapon(int weapon, int ammo)
    {
        switch (weapon)
        {
            case 1:
                // ammo > 0 ? Debug.Log(string.Format(" Firing weapon 1. It has {0} ammo left", ammo)) : Debug.Log("Out of ammo!");
                if(ammo > 0)
                {
                    Debug.Log(string.Format(" Firing weapon 1. It has {0} ammo left", ammo));

                }
                else
                {
                    Debug.Log("Out of ammo!");
                }

                break;

            default:
                break;
        }

       
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
            move(Vector3.forward);
        if (Input.GetKey(KeyCode.D))
            move(Vector3.right);
        if (Input.GetKey(KeyCode.A))
            move(-Vector3.right);
        if (Input.GetKey(KeyCode.S))
            move(-Vector3.forward);
    }
}
