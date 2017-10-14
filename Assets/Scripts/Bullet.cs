using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed = 20.0f;
    private int fireDirection = 1;
    private GameObject Player;
    private bool hasFired = false;

    public int FireDirection
    {
        get { return fireDirection; }
        set { fireDirection = value; }
    }

    public void move(Vector3 moveAmt)
    {
        hasFired = true;
        transform.position += moveAmt * speed * Time.deltaTime;
    }
    // Update is called once per frame

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update ()
    {
        if (hasFired == false)
            FireDirection =  Player.GetComponent<player>().FireDirection;
        if (fireDirection == 1 )
          move(Vector3.right);
        else if (fireDirection == 2)
            move(-Vector3.right);
        else if (fireDirection == 3)
            move(Vector3.up);
        else if (fireDirection == 4)
            move(-Vector3.up);
      

    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
