using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Teleport destination = null;

    public bool teleported = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!teleported)
        {
            other.gameObject.transform.position = destination.transform.position;
            destination.teleported = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        teleported = false;
    }
}
