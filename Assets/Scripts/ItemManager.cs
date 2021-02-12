using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject cloudIcon;

    private Vector2 poolPosition = new Vector2(0, -25);


    // Start is called before the first frame update
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
