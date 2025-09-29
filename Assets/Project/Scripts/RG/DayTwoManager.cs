using System.Collections;
using UnityEngine;

public class DayTwoManager : Singleton<DayTwoManager>
{
    [SerializeField] private bool ritualIsInsideTheHouse;
    [SerializeField] private bool fuseIsOn;
    private bool fuseBoxIsTurnedOn;
    [SerializeField] private bool realRitualWasBought;


    IEnumerator Start()
    {
        yield return new WaitUntil(() => ritualIsInsideTheHouse);
        //Cerrar puerta principal
        //Coloca las velas
        yield return new WaitUntil(() => fuseBoxIsTurnedOn);
        yield return new WaitUntil(() => realRitualWasBought);
    }
}
