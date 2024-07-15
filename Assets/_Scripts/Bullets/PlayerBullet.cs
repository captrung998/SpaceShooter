using System.Collections;
using UnityEngine;

public class PlayerBullet : Bullet
{
    private Vector3 _direction;

    public override void Move(Vector3 direction)
    {
        base.Move(direction);
        _direction = direction;

        StopAllCoroutines();
        StartCoroutine(MoveAnimation());
    }

    private IEnumerator MoveAnimation()
    {
        while (true)
        {
            transform.Translate(_direction * Time.deltaTime * speed);
            yield return null;
        }
    }
}
