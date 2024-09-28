using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce;
    Rigidbody2D rig;
    bool isGrounded;
    bool facingRight = false;
    SpriteRenderer SpriteCharac;
    public GameObject deathScreen;
    public GameObject GeneralUI;
    public Text playerScoreText;
    public Text playerCoinsText;
    public int playerScore;
    public int playerCoins;
    int timesVar = 0;
    public float blockTimer;

    private void Awake()
    {
        SpriteCharac = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var inputMove = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(inputMove, 0, 0);
        transform.position += move * speed * Time.deltaTime;

        if (!facingRight && inputMove > 0)
        {
            Flip();
        }else if(facingRight && inputMove < 0)
        {
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rig.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            //Debug.Log("Funcionando!");
            
        }
        blockTimer += Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else{
            isGrounded = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ExtraCoin")){
            
            if(timesVar <= 0)
            {
                playerCoins++;
                playerScore += 200;
                playerCoinsText.text = $"Coins: {playerCoins}";
                playerScoreText.text = $"Score:\n{playerScore}";
                timesVar += 1;
                blockTimer = 0;
            }
            
            if(blockTimer >= 1 && timesVar >= 1)
            {
                playerCoins++;
                playerScore += 200;
                playerCoinsText.text = $"Coins: {playerCoins}";
                playerScoreText.text = $"Score:\n{playerScore}";
                blockTimer = 0;
            }
        }
        else if (collision.CompareTag("Death"))
        {
            GiveDeathScreen();
        }
    }

    void GiveDeathScreen()
    {
        deathScreen.SetActive(true);
        GeneralUI.SetActive(false);
        Time.timeScale = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            GiveDeathScreen();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        SpriteCharac.flipX = facingRight;
    }
}
