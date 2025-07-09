using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public List<GunSpec> guns;
    public GunSpec currentGun;
    bool isReloading;
    public ShootingController shootingController;
    public AudioSource au;
        private void Start()
    {
        SwitchGun(guns[0]);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(guns[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(guns[1]);
        }
        if (currentGun == null) return;
        if (isReloading) return;
        if(currentGun.ammoInMag<currentGun.magSize && Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
            Invoke(nameof(Reload), currentGun.reloadTime);
            return;
        }
        currentGun.currentFireTime -= Time.deltaTime;
        if (currentGun.isAutomatic)
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
    void SwitchGun(GunSpec gun)
    {
        currentGun = gun;
    }
    void Fire()
    {
        if (currentGun.currentFireTime > 0) return;// check for firerate
        currentGun.currentFireTime = currentGun.firerate;// set firerate time
        currentGun.ammoInMag -= 1;// decreasing 1 bullet from mag
        shootingController.GenerateBullet();
        au.PlayOneShot(currentGun.fireSoundClip); // playing sound of fire
        if(currentGun.ammoInMag <= 0) // reload if bullets are zero in mag
        {
            //Reload();
            //Invoke("Reload",currentGun.reloadTime);
            isReloading = true;
            Invoke(nameof(Reload),currentGun.reloadTime);
        }
    }

    private void Reload()
    {
        currentGun.ammoInMag = currentGun.magSize;
        isReloading = false;
    }
}
// Car is a class
// class = blueprint/map/mold
[System.Serializable]
public class GunSpec
{
    public string name;
    public int magSize;
    public int ammoInMag;
    public float currentFireTime;
    public float firerate;
    public float range;
    public float weight;
    public float reloadTime;
    public bool isAutomatic;
    public GunType gunType;
    public Scope maxScopeAllowed;
    public Scope scope;
    public AudioClip fireSoundClip;
}
public enum Scope
{
    none,holographic,redDot,oneX,twoX,threeX,fourX,sixX,eightX
}
public enum GunType
{
    Pistol, Rifle, Sniper, Shotgun, Dmr, Machinegun, Smg
}
