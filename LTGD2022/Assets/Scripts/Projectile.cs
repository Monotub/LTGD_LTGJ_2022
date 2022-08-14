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
        Vector3 targetPos = new Vector3(target.x, target.y + 1f, target.z);
        if(transform != null)
            transform.DOMove(targetPos, speed * Time.deltaTime).SetEase(Ease.Linear);
    }

    public void SetParameters(Transform _target, int _damage, float _speed)
    {
        target = _target.position;
        damage = _damage;
        speed = _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Turret>() != null) return;

        if(other.TryGetComponent(out Health health))
        {
            health.DOKill();
            health.ProcessHit(damage);
            //DOTween.KillAll();
            Destroy(gameObject);
        }
    }
}
