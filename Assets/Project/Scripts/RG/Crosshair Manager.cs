using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UIElements;

public class CrosshairManager : Singleton<CrosshairManager>
{
    public Image Crosshair;
    public void TurnOn()
    {
        Crosshair.gameObject.SetActive(true);
    }
    public void TurnOff()
    {
        Crosshair.gameObject.SetActive(false);
    }
}
