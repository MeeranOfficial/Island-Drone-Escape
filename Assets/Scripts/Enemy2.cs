using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour {

    public float speed;
    public float turnSpeed = 10f;
    public float stoppingDistance;
    public float retreatDistance;

    public float count;
    

    private Transform player;
    public Transform enemy;
    
    public float fireRate = 1f;
    private float fireCountdown = 0f;
    

    public GameObject bulletPrefab;
    public Transform firePoint;

    
    void Start() {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }
    void Update() {

        this.transform.LookAt(player.transform);

        if (Vector3.Distance(transform.position, player.position) > stoppingDistance) {

            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        else if (Vector3.Distance(transform.position, player.position) < stoppingDistance && Vector3.Distance(transform.position, player.position) > retreatDistance) {

            transform.position = this.transform.position;
        }

        else if (Vector3.Distance(transform.position, player.position) < retreatDistance) {

            transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

     /*   Vector3 dir = player.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(enemy.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        enemy.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    */

        if(fireCountdown <= 0f) {

            Shoot();
            fireCountdown = 1f / (this.count + 1);
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot() {

        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Seek(player);
    }

    public void setCount(float count) {
        this.count = count;
    }

}
