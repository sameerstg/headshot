
using Photon.Pun;
using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
     PhotonView view;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
    void Update()
    {
        transform.Translate(transform.forward * Time.deltaTime*3);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            //collision.collider.GetComponent<PlayerModel>();
        }
        Destroy(this.gameObject);
    }

}
