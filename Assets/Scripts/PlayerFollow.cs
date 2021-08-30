using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Make the camera follow the player and turn around the player when the mouse move.<
/// </summary>
public class PlayerFollow : MonoBehaviour
{

    [SerializeField]
    private Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [SerializeField]
    private float SmoothFactor = 0.5f;

    [SerializeField]
    private float RotationSpeed = 5.0f;

    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    void LateUpdate()
    {
        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationSpeed, Vector3.up);

        _cameraOffset = camTurnAngle * _cameraOffset;

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);

        transform.LookAt(PlayerTransform);

    }
}
