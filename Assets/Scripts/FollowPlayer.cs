using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;
    private const float CamDistance = -10;

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = player.position + new Vector3(0, 0, CamDistance);
    }
}
