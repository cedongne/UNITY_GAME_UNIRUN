using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject cloudIcon;

    private Vector2 poolPosition = new Vector2(0, -25);

    public void OnEnable()
    {
        if(Random.Range(0, 7) == 0)
        {
            cloudIcon.SetActive(true);
        }
        else
        {
            cloudIcon.SetActive(false);
        }
    }
}
