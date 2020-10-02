using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        Bird player = collision.gameObject.GetComponent<Bird>();
        if (player != null)
        {
            player.isInvincible = true;
            if (!GameContol.instance.isAttacking)
                GameContol.instance.hit();
        }
       
    }
}
