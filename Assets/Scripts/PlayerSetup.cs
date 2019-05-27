using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    public Behaviour[] componentsToDisable;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<CompleteProject.CameraController>().setTarget(gameObject.transform);
    }
}
