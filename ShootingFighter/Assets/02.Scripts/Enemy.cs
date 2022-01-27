using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform tr;
    [SerializeField] float damage;
    [SerializeField] float speed;
    [SerializeField] float score;
    Vector3 dir;
    Vector3 deltaMove;
    [SerializeField] GameObject explosionEffect;
    private void Awake()
    {
        tr = gameObject.GetComponent<Transform>();
    }
    private void Start()
    {
        SetTarget_RandomlyToPlayer(30);
    }
    private void Update()
    {
        deltaMove = dir * speed * Time.deltaTime;
        tr.Translate(deltaMove);
    }
    private void SetTarget_RandomlyToPlayer(int percent)
    {
        int tmpRandomValue = Random.Range(0, 100);
        if(percent > tmpRandomValue)
        {
            GameObject target = GameObject.Find("Player");
            if(target == null)
            {
                dir = Vector3.back;
            }
            else
            {
                dir = (target.transform.position - tr.position).normalized;
            }
        }
        else
        {
            dir = Vector3.back;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))  // enum 사용법 추가 설명
        {
            GameObject effectGO = Instantiate(explosionEffect);
            effectGO.transform.position = tr.position;
            Player.instance.score += score;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if( collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameObject effectGO = Instantiate(explosionEffect);
            effectGO.transform.position = tr.position;
            collision.gameObject.GetComponent<Player>().HP -= damage;
            Destroy(this.gameObject);
        }
    }
}
