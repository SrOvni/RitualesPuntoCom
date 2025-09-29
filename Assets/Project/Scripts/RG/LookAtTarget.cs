using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float yOffset = 0.5f;
    [SerializeField] private float pivotSpeedRotation = 10;
    private void Start() {
        if(_target == null)
        {
            _target = Camera.main.transform;
        }
    }
    private void Update() {
        if(_target == null)return;
        Vector3 direction = _target.position - transform.position;
        direction.y += yOffset;
        Quaternion lookDirection = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation,lookDirection, Time.deltaTime * pivotSpeedRotation);
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LoseTarget()
    {
        _target = null;
    }
}
