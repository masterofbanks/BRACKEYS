using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrankGameManager : MiniGameManager
{
    [Header("Game Settings")]
    public int spinGoal = 6;

    [Header("Crank Collisions")]
    public GameObject[] gameObjects;

    [Header("Wheel Prefab")]
    public GameObject wheel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("We gettting here");
    }
}
