using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘
   public static Vector2 playerPosition;

   private int jumpCount = 0; // 누적 점프 횟수. 다단 점프 기능 구현을 위함
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태. 죽은 상태에서 또 죽는 상황을 방지.

   public static bool cloudItem;

   public static Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

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