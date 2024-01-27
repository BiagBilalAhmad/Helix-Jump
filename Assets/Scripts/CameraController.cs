using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform Player;
    public Vector3 Offset;
    public float SmoothSpeed;

    private void Start()
    {
        Player =GameObject.FindWithTag("Player").transform;
    }
    public float PositionOffset;
    private void FixedUpdate()
    {
        Vector3 TargetPos = Player.position + Offset;

        if (Player.position.y < transform.position.y+ PositionOffset)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPos, SmoothSpeed);

        }
    }
}
