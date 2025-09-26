using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public enum GizmosShape
    {
        Cube, Sphere, Mesh
    }
    public GizmosShape shape;
    public Color color;
    [SerializeField] float size = 2;
    void OnDrawGizmos()
    {
        switch (shape)
        {
            case GizmosShape.Cube:
                Gizmos.color = color;
                Gizmos.DrawCube(transform.position, Vector3.one * size);
                break;
        }
    }
}
