using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabScript : MonoBehaviour
{
    //private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //public void Init()
    //{
    //    gameManager = GameManager.instance;
    //}

    // Update is called once per frame
    void Update()
    {
        //gameManager = FindObjectOfType<GameManager>();
        GameManager.instance.Manage();

        //if (gameManager)
        //{
        //}
    }
}
