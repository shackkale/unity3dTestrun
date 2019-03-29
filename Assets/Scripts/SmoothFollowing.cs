using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowing : MonoBehaviour
{
    public GameObject target;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _offset;

    [SerializeField]
    [Range(0f, 1f)]
    private float _smoothTime;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - target.transform.position;
    }

    // Called at constant time interval
    void FixedUpdate()
    {
        if (target)
        {
            // Vector3 targetPosition = target.transform.TransformPoint(new Vector3(0, 3, -11.5f));
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + _offset, ref _velocity, _smoothTime);
        }
    }
}
