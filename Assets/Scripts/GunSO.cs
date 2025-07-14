using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun",menuName ="Gun",order =-1)]
public class GunSO : ScriptableObject
{
    public GunType type;
    public GunWeight weight;
    public List<FiringMode> firingMode;
    public int magSize;// magzine size
    public int maxAmmo;// max ammo player can carry
    public float reloadTime;
    public float firerate;
    public float damage;
}
public enum FiringMode
{
    single,burst,automatic
}
public enum GunType
{
    Pistol, Rifle, Sniper, Shotgun, Dmr, Machinegun, Smg
}
public enum GunWeight
{
    light, medium, heavy
}
