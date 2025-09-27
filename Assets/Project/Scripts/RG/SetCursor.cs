using UnityEngine;

[CreateAssetMenu(fileName = "Set cursor")]
public class SetCursor : ScriptableObject
{
    [SerializeField] private Texture2D cursorTexture;
    [ContextMenu("Cambiar cursor de juego")]
    public void Set()
    {
        Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.Auto);
    }
}
