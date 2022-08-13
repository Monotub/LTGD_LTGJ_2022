using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Projectile : MonoBehaviour
{
    Vector3 target;
    float speed = 5;
    int damage;


    private void Start()
    {
        transform.DOMove(target, speed * Time.deltaTime).SetEase(Ease.Linear);
    }

    public void SetParameters(Transform _target, int _damage, float _speed)
    {
        target = _target.position;
        damage = _damage;
        speed = _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Insect insect))
        {
            insect.ProcessHit(damage);
            DOTween.KillAll();
            Destroy(gameObject);
        }
    }

}
