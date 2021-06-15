using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        return;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Manages Game");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Managing");
    }

    public void Manage()
    {
        Debug.Log("Manage request");
    }
}
