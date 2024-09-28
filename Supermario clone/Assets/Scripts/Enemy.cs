using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float moveTimer;
    private Rigidbody2D rig;

    [SerializeField]bool facingRight;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {

            rig.velocity = new Vector2(speed, rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(-speed, rig.velocity.y);
        }

        timer += Time.deltaTime;
        if(timer > moveTimer)
        {
            facingRight = !facingRight;
            timer = 0f;  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
