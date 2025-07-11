
using Photon.Pun;
using System.Collections;
using UnityEngine;
public class Bullet : MonoBehaviour
{
     PhotonView view;
    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }
    IEnumerator Start()
    {
        yield return new WaitForSeconds(5);
        if(PhotonNetwork.InRoom)
            if (view.IsMine)
                PhotonNetwork.Destroy(gameObject);
    }
    void Update()
    {
        //transform.Translate(transform.forward* Time.deltaTime*3);
        if (view.IsMine)
        {
            transform.position += transform.forward * Time.deltaTime*5;
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (PhotonNetwork.InRoom)
    //        if (view.IsMine)
    //    PhotonNetwork.Destroy(gameObject);
    //}

}
