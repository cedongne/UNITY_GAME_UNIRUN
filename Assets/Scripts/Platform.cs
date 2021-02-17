using UnityEngine;

public class Platform : MonoBehaviour {
    public GameObject[] obstacles; 
    private bool stepped = false; 

    private void OnEnable() {
        stepped = false;

        for(int count = 0; count < obstacles.Length; count++)
        {
            if(Random.Range(0, 3) == 0)
            {
                obstacles[count].SetActive(true);
            }
            else
            {
                obstacles[count].SetActive(false);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Player" && !stepped)
        {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}