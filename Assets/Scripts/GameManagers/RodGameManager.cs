using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodGameManager : MiniGameManager
{
    public GameObject broken_rod_prefab;
    public GameObject horizontal_rod_prefab;

    public Transform broken_orig_position;
    public Transform horizontal_orig_position;

    public GameObject local_broken_rod;
    public GameObject local_horizontal_rod;

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
        cam.SetActive(true);
        local_broken_rod = Instantiate(broken_rod_prefab, broken_orig_position.position, broken_orig_position.rotation);
        local_broken_rod.transform.parent = gameObject.transform;
        local_horizontal_rod = Instantiate(horizontal_rod_prefab, horizontal_orig_position.position, horizontal_orig_position.rotation);
        local_horizontal_rod.transform.parent = gameObject.transform;
    }

    private void OnDisable()
    {
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().SwitchCamTo(GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().currentCamIndex);
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame = false;
        Destroy(local_broken_rod);
        Destroy(local_horizontal_rod);
        
    }
}
