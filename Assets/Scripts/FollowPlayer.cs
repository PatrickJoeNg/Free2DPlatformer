using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
