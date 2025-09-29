using System.Collections;
using UnityEngine;

public class SecondDayManager : Singleton<SecondDayManager>
{
    [SerializeField] private float longHorseDreamDuration;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(longHorseDreamDuration);
    }
}
