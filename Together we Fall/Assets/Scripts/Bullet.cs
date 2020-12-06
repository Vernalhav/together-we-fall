using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    [SerializeField] private Rigidbody2D rigidBody;
    private string owner;

    public virtual void Shoot(Transform _target, float _speed, float _dmg, string _owner){
        Vector3 _dir = _target.position - transform.position;
        Vector2 direction = new Vector2(_dir.x, _dir.y).normalized;
        owner = _owner;
        rigidBody.velocity = direction * _speed;
        damage = _dmg;
    }
    
    void OnTriggerEnter2D(Collider2D other){
        // TODO: Refactor!
        if(owner != "Soldier" && (other.gameObject.CompareTag("Soldier") || 
                                    other.gameObject.CompareTag("Tank") || 
                                    other.gameObject.CompareTag("Runner") || 
                                    other.gameObject.CompareTag("Irene")))
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
