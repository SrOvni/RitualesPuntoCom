using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private float horseDreamDuration;
    [SerializeField] private bool fakeRitualWasBought;
    public bool FakeRitualWasBought { get => fakeRitualWasBought; set => fakeRitualWasBought = value; }
    [SerializeField] private bool realRitualWasBought;
    [SerializeField] private bool playerAte;
    [SerializeField] private bool ritualIsInsideTheHouse;
    [SerializeField] private bool fakeRitualFinished;
    [SerializeField] private bool fuseIsOn;
    [SerializeField] private float longHorseDreamDuration;
    private bool fuseBoxIsTurnedOn;

    void Awake()
    {
        StartCoroutine(HorseDream());
    }

    public void Log()
    {
        Debug.Log("Funciona el boton");
    }
    public IEnumerator HorseDream()
    {
        yield return new WaitForSeconds(horseDreamDuration);
    }
    public IEnumerator FirstDay()
    {
        //Sueño
        //Ingresa moneda
        //Sube al caballo
        //Luz se apaga - 5s
        
        //Despierta en su cuarto
        yield return new WaitUntil(() => fakeRitualWasBought);
        //Desbloquear puerta de su cuarto
        //NO se puede abrir la puerta principal
        yield return new WaitUntil(() => playerAte);
        //Suena el timbre
        //Abrir puerta //Poner un collider para que no salga el jugador
        yield return new WaitUntil(() => ritualIsInsideTheHouse);
        //Cerrar puerta principal
        //Coloca las velas
        yield return new WaitUntil(() => fakeRitualFinished);
        //Luces se apagan
        yield return new WaitUntil(() => fuseBoxIsTurnedOn);
        yield return new WaitUntil(() => realRitualWasBought);
        //Se va dormir
        //Cambiar escena
    }
    public IEnumerator SecondDay()
    {
        //Long horse dream
        yield return new WaitForSeconds(longHorseDreamDuration);

    }
}

[CreateAssetMenu(fileName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] Text text;
}
