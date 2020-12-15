using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    private Transform target;
    
    public float speed = 70f;
    public GameObject impactEffect;
    private float maxDist = 0.1f;

    private GameObject triggeringPlayer;
    public float damage;

    
   
    public void Seek(Transform _target) {

        target = _target;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDist += 1 * Time.deltaTime;

        if (maxDist >= 1)
            HitTarget();

        /*if (target == null) {

            Destroy(gameObject);
            return;
        } */

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if(dir.magnitude < distanceThisFrame) {

            HitTarget();
            return;
        }

        //transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    void HitTarget() {

        GameObject effectIns = (GameObject) Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);

        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other) {
        
        if(other.tag == "Player") {

            triggeringPlayer = other.gameObject;
            triggeringPlayer.GetComponent<PlayerController>().health -= damage;

            triggeringPlayer.GetComponent<PlayerController>().healthBar.fillAmount = triggeringPlayer.GetComponent<PlayerController>().health / triggeringPlayer.GetComponent<PlayerController>().startHealth;
        }
    }
}
