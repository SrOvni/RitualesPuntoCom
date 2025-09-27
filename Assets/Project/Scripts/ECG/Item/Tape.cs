using UnityEngine;

public class Tape : Item, IUsable
{
    public void Use(GameObject user)
    {
        Debug.Log("Usandoooo");
    }
}
