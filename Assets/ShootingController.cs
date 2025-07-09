using UnityEngine;
public class ShootingController : MonoBehaviour
{
   
    public Transform coil;
    public GameObject bullet;
    public float bulletSpeed;
    public void GenerateBullet()
    {
        var newBullet = Instantiate(bullet, coil.transform.position, coil.transform.rotation);
        var rigidbody = newBullet.GetComponent<Rigidbody>();
            rigidbody.AddForce(newBullet.transform.up * bulletSpeed, ForceMode.Impulse);
    }
    private void FixedUpdate()
    {
        Debug.LogError(Time.fixedDeltaTime);
    }
}
