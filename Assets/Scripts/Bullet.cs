using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float speed = 20.0f;
    int fireDirection = 1;

    public void move(Vector3 moveAmt)
    {
        transform.position += moveAmt * speed * Time.deltaTime;
    }
    // Update is called once per frame
    void Update () {
       if (fireDirection == 1 )
          move(Vector3.right);
            }
}
