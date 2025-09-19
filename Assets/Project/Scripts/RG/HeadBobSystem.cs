using System;
using UnityEngine;

public class HeadBobSystem : MonoBehaviour
{
    InputReader input;

    private void Awake()
    {
        input.EnablePlayerInputActions();
    }

    void Update()
    {
        CheckForHeadBobTrigger();
    }
    private void CheckForHeadBobTrigger()
    {
        float inputMagnitude = new Vector3(input.Direction.x, 0, input.Direction.y).magnitude;
        if (inputMagnitude > 0)
        {
            StartHeadBob();
        }
    }

    private void StartHeadBob()
    {
        throw new NotImplementedException();
    }
}
