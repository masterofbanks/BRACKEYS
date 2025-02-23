using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RodGameManager : MiniGameManager
{
    public GameObject broken_rod_prefab;
    public GameObject horizontal_rod_prefab;

    public Transform broken_orig_position;
    public Transform horizontal_orig_position;

    public GameObject local_broken_rod;
    public GameObject local_horizontal_rod;

    public int numHitsOnWalls;
    public GameObject warning;
    public Transform[] WarningPosition; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        local_broken_rod = Instantiate(broken_rod_prefab, broken_orig_position.position, broken_orig_position.rotation);
        local_broken_rod.transform.parent = gameObject.transform;
        local_horizontal_rod = Instantiate(horizontal_rod_prefab, horizontal_orig_position.position, horizontal_orig_position.rotation);
        local_horizontal_rod.transform.parent = gameObject.transform;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Destroy(local_broken_rod);
        Destroy(local_horizontal_rod);
        numHitsOnWalls = 0;


    }

    public void YouveHitAWall()
    {
        numHitsOnWalls++;
        if (numHitsOnWalls == 4)
        {
            SceneManager.LoadScene(1);
        }

        else
        {
            Instantiate(warning, WarningPosition[numHitsOnWalls - 1].position, WarningPosition[numHitsOnWalls - 1].rotation);

        }
    }
}
