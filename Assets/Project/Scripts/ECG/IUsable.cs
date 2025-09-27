using UnityEngine;

public interface IUsable 
{
    // The GameObject that is using the item (i.e., the player)
    void Use(GameObject user);
}
