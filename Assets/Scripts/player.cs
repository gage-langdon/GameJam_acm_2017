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



    private float speed = 3.0f;
    private int health = 5;
    private int ammo;
    private int weapon = 1;
    private float nextFire = 0.0F;
    public GameObject TheActualBullet;
    public Transform BulletSpawnRight;
    public Transform BulletSpawnLeft;
    public Transform BulletSpawnUp;
    public Transform BulletSpawnDown;

    private int fireDirection = 1;





    public struct WeaponSystem
    {
        public int weapon, ammo;
        public float fireRate;

        public WeaponSystem(int x1, ref int x2, float x3)
        {
            weapon = x1;
            ammo = x2;
            fireRate = x3;
        }
    }

    WeaponSystem Pistol;
    WeaponSystem MachineGun;

    public int  FireDirection
    {
        get { return fireDirection; }
        set { fireDirection = value; }
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

    private void FireWeapon( int weapon, int fireDirection)
    {
        if (weapon == 1)
            Fire(Pistol.weapon, ref Pistol.ammo, Pistol.fireRate, fireDirection);
        else if (weapon == 2)
            Fire(MachineGun.weapon, ref MachineGun.ammo, MachineGun.fireRate, fireDirection);
        else
            return;
    }

    private void Fire(int weapon, ref int ammo, float fireRate, int fireDirection)
    {
        Transform BulletSpawn;
        switch(fireDirection)
        {
            case 1:
                BulletSpawn = BulletSpawnRight;
                break;
            case 2:
                BulletSpawn = BulletSpawnLeft;
                break;
            case 3:
                BulletSpawn = BulletSpawnUp;
                break;
            default:
                BulletSpawn = BulletSpawnDown;
                break;
        }
        switch (weapon)
        {
            case 1:
                if(ammo > 0 && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    GameObject bullet = Instantiate(TheActualBullet, BulletSpawn.position, BulletSpawn.rotation);
                    
                    ammo--;
                    Debug.Log(string.Format(" Firing weapon 1. It has {0} ammo left", ammo));
                }
                else if (ammo > 0)
                {
                    Debug.Log("delay");
                }
                else
                {
                    Debug.Log("Out of ammo!");
                }

                break;

            case 2:
                if (ammo > 0 && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    Instantiate(TheActualBullet, BulletSpawn.position, BulletSpawn.rotation);
                    ammo--;
                    Debug.Log(string.Format(" Firing weapon 2. It has {0} ammo left", ammo));
                }
                else if (ammo > 0)
                {
                    Debug.Log("delay");
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

    

    private void Reload(int weapon)
    {
        switch(weapon)
        {
            case 1:
                Pistol.ammo = 8;
                Debug.Log("reloaded pistol");
                break;
            case 2:
                MachineGun.ammo = 20;
                Debug.Log("reloaded machine gun");
                break;
            default:
                Debug.Log("nothing reloaded");
                break;
        }
    }
   

    void Start()
    {
        
        Pistol.weapon = 1;
        Pistol.ammo = 8;
        Pistol.fireRate = 0.5f;

        MachineGun.weapon = 2;
        MachineGun.ammo = 20;
        MachineGun.fireRate = 0.1f;

        Weapon = 1;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            move(Vector3.up);
            FireDirection = 3;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move(Vector3.right);
            FireDirection = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move(-Vector3.right);
            FireDirection = 2;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move(-Vector3.up);
            FireDirection = 4;
        }
        if (Input.GetMouseButtonDown(0))
            FireWeapon(Weapon, FireDirection);
        if (Input.GetKeyDown(KeyCode.R))
            Reload(Weapon);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Weapon = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Weapon = 2;

        
    }


}
