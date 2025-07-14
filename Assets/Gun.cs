using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunSO gun;
    bool isReloading;
    public  int magAmmo;
    public int totalAmmo;
    public float currentFireTime;
    internal FiringMode firingMode;

    private void Awake()
    {
        firingMode = gun.firingMode[0];
        magAmmo = gun.magSize;
        totalAmmo = gun.maxAmmo;
    }
    private void Update()
    {
        currentFireTime -= Time.deltaTime;
        if (gun == null) return;
        if (isReloading) return;
        if (CanReload()&& Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            Invoke(nameof(Reload), gun.reloadTime);
            return;
        }
        currentFireTime -= Time.deltaTime;
        if (firingMode == FiringMode.automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

    }
    public bool CanFire()
    {
        return currentFireTime <=0 && magAmmo > 0 && !isReloading ;
    }
    public void Fire()
    {
        if (!CanFire()) return;
        currentFireTime = gun.firerate;// set firerate time
        magAmmo-= 1;// decreasing 1 bullet from mag
        if (magAmmo<= 0 && totalAmmo>0) // reload if bullets are zero in mag
        {
            isReloading = true;
            Invoke(nameof(Reload), gun.reloadTime);
        }
    }
    public bool CanReload()
    {
        return magAmmo < gun.magSize && totalAmmo > 0;
    }
    private void Reload()
    {
        var diff = gun.magSize - magAmmo;
        var toAdd = gun.magSize - diff;
        if (totalAmmo >= gun.magSize)
        {
            magAmmo += toAdd;
            totalAmmo -= toAdd;
        }
        else if(totalAmmo< toAdd)
        {
            magAmmo += totalAmmo;
            totalAmmo = 0;
        }
        isReloading = false;
    }
}
