using DesignPatterns;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineCamera computerCamera;
    [SerializeField] CinemachineCamera playerCamera;
    public void PrioritizeComputerCamera()
    {
        computerCamera.Prioritize();
    }
    public void PrioritizePlayerCamera()
    {
        playerCamera.Prioritize();
    }
}
