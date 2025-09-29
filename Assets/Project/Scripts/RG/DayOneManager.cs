using System.Collections;
using UnityEngine;

public class DayOneManager : Singleton<DayOneManager>
{
    [SerializeField] private bool fakeRitualWasBought;
    public bool FakeRitualWasBought { get => fakeRitualWasBought; set => fakeRitualWasBought = value; }
    [SerializeField] private bool realRitualWasBought;
    [SerializeField] private bool playerAte;
    public bool PlayerAte { get => playerAte; set => playerAte = value; }
    private bool fuseBoxIsTurnedOn;

    IEnumerator Start()
    {
        
        //Despierta en su cuarto
        yield return new WaitUntil(() => fakeRitualWasBought);
        //Desbloquear puerta de su cuarto
        //NO se puede abrir la puerta principal
        yield return new WaitUntil(() => playerAte);
        //Suena el timbre
        //Abrir puerta //Poner un collider para que no salga el jugador
        
        //Luces se apagan
        yield return new WaitUntil(() => fuseBoxIsTurnedOn);
        yield return new WaitUntil(() => realRitualWasBought);
        //Se va dormir
        //Cambiar escena
        yield return null;
    }
}
public class DayTwoManager : Singleton<DayTwoManager>
{
    [SerializeField] private bool ritualIsInsideTheHouse;
    [SerializeField] private bool fakeRitualFinished;
    [SerializeField] private bool fuseIsOn;

    IEnumerator Start()
    {
        yield return new WaitUntil(() => ritualIsInsideTheHouse);
        //Cerrar puerta principal
        //Coloca las velas
        yield return new WaitUntil(() => fakeRitualFinished);
    }
}
