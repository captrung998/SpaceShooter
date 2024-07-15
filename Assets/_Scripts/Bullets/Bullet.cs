using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float speed = 5f;

    public virtual void Move(Vector3 direction)
    {
     
    }
}
