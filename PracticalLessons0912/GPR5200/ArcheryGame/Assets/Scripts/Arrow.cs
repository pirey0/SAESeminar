using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float damage = 10;
    [SerializeField] AnimationCurve followDirectionCurve;
    [SerializeField] string[] effects;
    [SerializeField] float lifeTime;

    Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (lifeTime > 0)
            Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        float strength = followDirectionCurve.Evaluate(_rigidbody.velocity.magnitude);
         transform.forward =Vector3.Lerp(transform.forward, _rigidbody.velocity, strength * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.collider.GetComponent<IDamagable>();
        if (damagable != null)
        {
            if(damage > 0)
            {
                var result = damagable.TakeDamage(damage);

                if (result == TakeDamageResult.Destroy)
                {
                    Destroy(gameObject);
                }
                else if(result == TakeDamageResult.Stuck)
                {
                    transform.parent = collision.transform;
                    Destroy(this);
                    Destroy(GetComponent<Rigidbody>());
                    Destroy(GetComponent<Collider>());
                }
            }

            IEffectedDamagable effectedDamagable = damagable as IEffectedDamagable;
            if(effectedDamagable != null)
            {
                EffectParams parameters = new EffectParams(collision.gameObject, effectedDamagable);
                foreach (var effect in effects)
                {
                    if (!effectedDamagable.IgnoresEffect(effect))
                    {
                        EffectHandler.Instance.ApplyEffect(effect, parameters);
                    }
                }
            }
        }
    }
}
