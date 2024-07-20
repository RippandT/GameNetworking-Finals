using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBoundsRespawn : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    void OnTriggerEnter(Collider other) {
        other.gameObject.transform.position = spawnPoint.transform.position;
    }
}
