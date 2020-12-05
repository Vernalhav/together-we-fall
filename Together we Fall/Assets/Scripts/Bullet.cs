using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    [SerializeField] private Rigidbody2D rigidBody = null;
    private string owner;

    void Start()
    {
        // rigidBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Shoot(Transform _target, float _speed, float _dmg, string _owner){
        Vector3 _dir = _target.position - transform.position;
        Vector2 direction = new Vector2(_dir.x, _dir.y).normalized;
        owner = _owner;
        rigidBody.velocity = direction * _speed;
        damage = _dmg;
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Soldier") && owner != "Soldier")
        {
            other.gameObject.GetComponentInParent<Soldier>().ReceiveDamage(damage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Enemy") && owner != "Enemy")
        {
            other.gameObject.GetComponentInParent<Enemy>().ReceiveDamage(damage);
            Destroy(gameObject);
        }
    }
}
