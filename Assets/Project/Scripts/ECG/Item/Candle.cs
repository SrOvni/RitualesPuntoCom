using UnityEngine;

public class Candle : MonoBehaviour
{
    // Una referencia al padre de los sistemas de part�culas
    [SerializeField] private GameObject flameParticleParent;

    private bool isLit = false;

    // Un array para almacenar todos los sistemas de part�culas hijos
    private ParticleSystem[] childParticleSystems;

    private void Awake()
    {
        // Al inicio, encuentra todos los sistemas de part�culas en los hijos
        // del objeto padre y los guarda en un array.
        if (flameParticleParent != null)
        {
            childParticleSystems = flameParticleParent.GetComponentsInChildren<ParticleSystem>();
        }
    }

    public void LightCandle()
    {
        if (flameParticleParent != null && !isLit)
        {
            // Reproduce cada sistema de part�culas en el array
            foreach (ParticleSystem ps in childParticleSystems)
            {
                ps.Play(true); // El 'true' asegura que si hay subsistemas, tambi�n se reproduzcan.
            }
            isLit = true;
        }
    }
}
