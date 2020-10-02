using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float upForce = 750f;
    public float speed = 5f;
    public bool isGround;
    public bool isJumping;
    public bool isGlide;
    public float jump_rec=0;
    private float glide_rec = 0; 

    public float timeInvincible = 3.0f;
    public bool isInvincible;
    float invincibleTimer;

    private float hit_area_count = 0;
    private float movement = 0f;
    //is on ground
    private bool is_on;
    private Rigidbody2D rb2d;
    public Animator anim;
    private Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        isGround = true;
        is_on = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameContol.instance.isDead == false)
        {
            movement = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(speed * movement, rb2d.velocity.y);

            if (movement < 0f && GameContol.instance.isAttacking == false)
            {
                trans.localScale = new Vector3(-0.42159f, trans.localScale.y, trans.localScale.z);
                if (!isGlide && is_on)
                {
                    anim.SetBool("walk", true);
                }
            }
            if (movement > 0f && GameContol.instance.isAttacking == false)
            {
                trans.localScale = new Vector3(0.42159f, trans.localScale.y, trans.localScale.z);
                if (!isGlide && is_on)
                {
                    anim.SetBool("walk", true);

                }
            }
            if (movement == 0f &&  GameContol.instance.isAttacking == false)
            {
                if (!isGlide)
                {
                    anim.SetBool("walk", false);
                    anim.SetTrigger("idel");
                }
            }


            if (Input.GetButtonDown("Jump")&& glide_rec==0 && jump_rec < 2)
            {
                
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, upForce));
                isGround = false;
                is_on = false;
                jump_rec++;
                if (!isGlide)
                {

                    anim.SetBool("jump2",true);

                }
                GameObject.Find("Sky (1)").GetComponent<AudioSource>().Play();
            }
            if(Input.GetButtonUp("Jump"))
            {
                isGround = true;
                anim.SetBool("jump2",false);
            }


            if (Input.GetButtonDown("Fire1") )
            {
                anim.SetBool("walk", false);
                GameContol.instance.isAttacking = true;
                anim.SetBool("attack2",true);
                GameObject.Find("Sky (2)").GetComponent<AudioSource>().Play();
            }
            if (Input.GetButtonUp("Fire1") )
            {
                GameContol.instance.isAttacking = false;
                anim.SetBool("attack2", false);
            }
           
            
            
            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetBool("glid", true);
                isGlide = true;
                 anim.SetBool("walk", false);
                anim.SetBool("jump2", false);

                //rb2d.AddForce(new Vector2(0, 500f));
                if (glide_rec < 1)
                {
                    glide_rec++;
                    GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                }
                
            }
            
            if (Input.GetButtonUp("Fire2"))
            {
                anim.SetBool("glid", false);
                isGlide = false;
                GetComponent<Rigidbody2D>().gravityScale = 4;
                
                //anim.SetBool("walk", false);
               
            }

        }
        else
        {
            rb2d.velocity = Vector2.zero;
            
            GameContol.instance.BirdDie();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

   
    
     private void OnCollisionEnter2D(Collision2D collision)
    {
        jump_rec = 0;
        is_on = true;
        glide_rec = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "dieArea" && hit_area_count <4)
        {
            rb2d.AddForce(new Vector2(0,1000f));
            hit_area_count++;
        }
    }
    


}
