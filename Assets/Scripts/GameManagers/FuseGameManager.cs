using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseGameManager : MiniGameManager
{
    public GameObject broken_fuse_prefab;
    public GameObject whole_fuse_prefab;

    public GameObject local_broken_fuse;
    public GameObject local_whole_fuse;

    public GameObject cut_whole_fuse;
    public GameObject cut_broken_fuse;

    public Transform broken_orig_position;
    public Transform whole_orig_position;

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
        cam.SetActive(true);
        //local_broken_fuse = Instantiate(broken_fuse_prefab, broken_orig_position.position, broken_orig_position.rotation);
        //local_broken_fuse.transform.parent = gameObject.transform;
        local_whole_fuse = Instantiate(whole_fuse_prefab, whole_orig_position.position, whole_orig_position.rotation);
        local_whole_fuse.transform.parent = gameObject.transform;
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().ActivateCamera(GameObject.FindWithTag("CameraManager").GetComponent<CameraManager>().currentCamIndex);
        GameObject.FindWithTag("Desk").GetComponent<DeskManager>().player.GetComponent<PlayerMovement>().enabled = true;
        GameObject.FindWithTag("GameController").GetComponent<GameManager>().inMinigame = false;
        cut_whole_fuse.SetActive(false);
        cut_broken_fuse.SetActive(true);
        base.OnDisable();
    }
}
