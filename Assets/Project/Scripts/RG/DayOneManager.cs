using System.Collections;
using UnityEngine;

public class DayOneManager : Singleton<DayOneManager>
{
    public Sprite E;
    [SerializeField] Collider PuertaJugador;
    [SerializeField] private bool fakeRitualWasBought;
    public bool FakeRitualWasBought { get => fakeRitualWasBought; set => fakeRitualWasBought = value; }
    [SerializeField] private bool fakeRitualFinished;
    [SerializeField] private bool playerAte;
    public bool PlayerAte { get => playerAte; set => playerAte = value; }

    IEnumerator Start()
    {
        //Despierta en su cuarto
        yield return new WaitUntil(() => fakeRitualWasBought);
        PuertaJugador.enabled = false;
        //Desbloquear puerta de su cuarto
        //NO se puede abrir la puerta principal
        yield return new WaitUntil(() => playerAte);
        //Suena el timbre\
        yield return new WaitUntil(() => fakeRitualFinished);
        //Abrir puerta //Poner un collider para que no salga el jugador

        //Luces se apagan

        //Se va dormir
        //Cambiar escena
        yield return null;
    }
}

// Extra: Hace que el Canvas siempre mire a la cámara principal
public class LookAtCamera : MonoBehaviour
{
    void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
        }
    }
}