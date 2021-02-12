using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour {
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text bestScoreText;
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject newRecordText;
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    public GameObject PauseText;
    public Sprite CloudItemUI_nohave;
    public Sprite CloudItemUI_disable;
    public Sprite CloudItemUI_usable;

    public Image cloudItemImage;

    private int score = 0; // 게임 점수
    private int bestScore = 0;

    private bool isPause = false;

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake() {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("UniRunBestScore");
        bestScoreText.text = "Best Score : " + bestScore;
    }

    void Update() {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main");
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            Pause(isPause);
        }
        if (PlayerController.cloudItem && PlayerController.playerRigidbody.velocity.y >= 0)
        {
            cloudItemImage.sprite = CloudItemUI_disable;
        }
        else if (PlayerController.cloudItem && PlayerController.playerRigidbody.velocity.y < 0)
        {
            cloudItemImage.sprite = CloudItemUI_usable;
        }
        else if (!PlayerController.cloudItem)
        {
            cloudItemImage.sprite = CloudItemUI_nohave;
        }
        if (score > bestScore)
        {
            newRecordText.SetActive(true);
        }

    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore) {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("UniRunBestScore", score);
        }
    }

    private void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 1;
            isPause = false;
            PauseText.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPause = true;
            PauseText.SetActive(true);
        }

    }

}