using UnityEngine;

public class PlayerController : MonoBehaviour {
   public AudioClip deathClip;
   public float jumpForce = 700f;
   public static Vector2 playerPosition;

   private int jumpCount = 0;
   private bool isGrounded = false;
   private bool isDead = false;

   public static bool cloudItem;

   public static Rigidbody2D playerRigidbody;
   private Animator animator;
   private AudioSource playerAudio;

   private void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();

        cloudItem = false;
   }

   private void Update()
    {
        playerPosition = transform.position;

        if (isDead)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
        {
            jumpCount++;

            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetKeyUp(KeyCode.Space) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cloudItem && playerRigidbody.velocity.y < 0f)
        {
            ItemUsing.instance.cloudGenerate();
            cloudItem = false;
        }
        animator.SetBool("OnPlat", isGrounded);
   }

   private void Die() {
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;
        
        cloudItem = false;

        GameManager.instance.OnPlayerDead();
   }

   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
        else if (other.tag == "Item")
        {
            other.gameObject.SetActive(false);
            cloudItem = true;
        }
   }

   private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        isGrounded = false;
   }
}