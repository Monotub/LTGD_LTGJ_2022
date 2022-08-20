using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Projectile : MonoBehaviour
{
    protected Vector3 target;
    protected int damage;
    protected float moveTime = 0;
    float speed;
    Vector3 startPos;


    protected void Start()
    {
        startPos = transform.position;
        transform.rotation = Quaternion.LookRotation(target - startPos);
    }

    protected void Update()
    {
        moveTime += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, target, moveTime);
    }


    public void SetParameters(Transform _target, int _damage, float _speed)
    {
        target = _target.position;
        damage = _damage;
        speed = _speed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.GetComponent<Turret>() != null) return;

        if (other.TryGetComponent(out Health health))
        {
            health.ProcessHit(damage);
            Destroy(gameObject);
        }
    }
}
