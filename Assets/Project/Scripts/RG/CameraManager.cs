using Unity.Cinemachine;
using UnityEngine;
using System.Collections;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] CinemachineCamera computerCamera;
    public CinemachineCamera ComputerCamera => computerCamera;
    public CinemachineBrain Brain => brain;
    [SerializeField] CinemachineCamera playerCamera;
    [SerializeField] CinemachineBrain brain;
    void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
    }
    public void PrioritizeComputerCamera()
    {
        computerCamera.Prioritize();
    }
    public void PrioritizePlayerCamera()
    {
        playerCamera.Prioritize();
    }
    public IEnumerator WaitForBlendEnd()
{
    // Espera hasta que deje de haber blend
    yield return new WaitUntil(() => brain.ActiveBlend == null);
    Debug.Log("¡Blend finalizado!");
}
}
