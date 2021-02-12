using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsing : MonoBehaviour
{
    public GameObject cloudPlatPrefab;
    private GameObject cloudPlat;

    public static ItemUsing instance;

    public Vector2 playerSpeed;
    public Vector2 playerPosition;

    private Vector2 poolPosition = new Vector2(0, -25);

    void Start()
    {
        instance = this;

        cloudPlat = Instantiate(cloudPlatPrefab, poolPosition, Quaternion.identity);
    }

    public void cloudGenerate()
    {
        playerSpeed = PlayerController.playerRigidbody.velocity;
        playerPosition = PlayerController.playerPosition;

        Debug.Log(playerSpeed);
        Debug.Log(playerPosition);

        cloudPlat.SetActive(false);
        cloudPlat.SetActive(true);
        cloudPlat.transform.position = new Vector2(playerPosition.x + 0.3f - (playerSpeed.y / 40f), playerPosition.y - 1.2f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Dead")
        {
            Debug.Log("Cloud Delete");
            cloudPlat.SetActive(false);
        }
    }
}
