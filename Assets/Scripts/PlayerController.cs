using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameManager gameManager;
    public float speed;
    public Text countText;
    public LayerMask groundLayer;
    public float jumpForce = 7;
    public SphereCollider col;
    public float startHealth = 100;
    public float health;

    private Rigidbody rb;
    private int count;

    public Image healthBar;

  

    void Start() {

        health = startHealth;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        count = 0;
        setCountText ();
    }
    void FixedUpdate() {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        rb.AddForce(movement * speed * Time.deltaTime); 


        //Jump
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space)) {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //EndGame
        if (rb.position.y < -5f) {

            FindObjectOfType<GameManager>().EndGame();
        }    

        if(health <= 0) {

            Die();
        }
    }

    public void Die() {

        Destroy(this.gameObject);
        FindObjectOfType<GameManager>().EndGame();
    }

    private bool IsGrounded() {

        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, 
            col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayer); ;
    }

    void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.CompareTag("Pick up")) {

            other.gameObject.SetActive(false);
            count++;
            FindObjectOfType<Enemy2>().setCount(count);
            setCountText();
        }
    }

    void setCountText() {

        countText.text = "Count: " + count.ToString() + " / 5";

        if (count == 5) {

            gameManager.CompleteLevel();
        }
    }

}
