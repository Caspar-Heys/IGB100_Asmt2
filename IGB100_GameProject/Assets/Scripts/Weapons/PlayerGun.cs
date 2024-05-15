using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    Animation animation;
    public GameObject uiController;
    //------------------------------------------------Rifle Variables
    public float Rifledmg;
    public float RiflefireRate;
    private float RiflefireTimer = 0.0f;

    public float Riflemagazine;
    public float RiflemaxMagazine;
    public float RiflereloadCD;
    private float RiflereloadTimer;
    public bool Riflereloading = false;

    //------------------------------------------------ShotGun variables
    public float ShotGunDmg;
    public float ShotGunfireRate;
    private float ShotGunfireTimer = 0.0f;

    public float ShotGunmagazine;
    public float ShotGunmaxMagazine;
    public float ShotGunreloadCD;
    private float ShotGunreloadTimer;
    public bool ShotGunreloading = false;
    public float BulletSpread;

    [SerializeField] private float BulletRange;
    [SerializeField] private int NumPellets;
    //------------------------------------------------RPG variables
    public float RPGdmg;
    public float RPGfireRate;
    private float RPGfireTimer = 0.0f;

    public float RPGmagazine;
    public float RPGmaxMagazine;
    public float RPGreloadCD;
    private float RPGreloadTimer;
    public bool RPGreloading = false;


    //------------------------------------------------Other
    public GameObject muzzleFlash;
    public GameObject bulletHit;
    public GameObject muzzle;
    public GameObject raycastTarget;

    //------------------------------------------------audio
    public GameObject fireSound;
    public GameObject fireSoundEmpty;
    public GameObject reloadSound;

    // weapon mode
    public int weaponMode = 1; // weaponMode = 1 --> full auto, weaponMode = 2 --> shotgun, weaponmode = 3 --> explosive rocket

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponFiring();
        Reload();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeaponMode();
        }

    }

    private void WeaponFiring()
    {
        if (weaponMode == 1)
        {
            if (Input.GetMouseButton(0) && Time.time > RiflefireTimer && !Riflereloading)
            {
                if (Riflemagazine > 0) // fire
                {
                    Instantiate(fireSound, transform.position, transform.rotation);
                    Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                    animation.Play("Fire");
                    Ray ray = new Ray(muzzle.transform.position, muzzle.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit HitInfo, 50f))
                    {
                        if (HitInfo.transform.tag == "Enemy")
                        {
                            HitInfo.transform.GetComponent<Enemy>().TakeDamage(Rifledmg);
                            Instantiate(bulletHit, HitInfo.transform.position, HitInfo.transform.rotation);
                        }
                    }
                    Riflemagazine--;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);
                    RiflefireTimer = Time.time + RiflefireRate;
                }
                else
                {
                    Instantiate(fireSoundEmpty, transform.position, transform.rotation);
                    RiflefireTimer = Time.time + RiflefireRate;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);
                }
            }
        }
        else if(weaponMode == 2)
        {
            if (Input.GetMouseButtonDown(0) && Time.time > ShotGunfireTimer && !ShotGunreloading)
            {
                if (ShotGunmagazine > 0) // fire
                {
                    Instantiate(fireSound, transform.position, transform.rotation);
                    Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                    animation.Play("Fire");
                    Ray[] Pellets = new Ray[NumPellets];
                    for (int i = 0; i < Pellets.Length; i++)
                    {
                        float spreadX = Random.Range(-BulletSpread, BulletSpread);
                        float spreadY = Random.Range(-BulletSpread, BulletSpread);
                        Vector3 spread = new Vector3(spreadX, spreadY, 0);
                        Pellets[i] = new Ray(muzzle.transform.position, muzzle.transform.forward + spread);
                        Debug.DrawLine(muzzle.transform.position, (muzzle.transform.forward + spread) * BulletRange, Color.yellow, 5.0f);
                        if (Physics.Raycast(Pellets[i], out RaycastHit HitInfo, BulletRange))
                        {
                            if (HitInfo.transform.tag == "Enemy")
                            {
                                HitInfo.transform.GetComponent<Enemy>().TakeDamage(ShotGunDmg);
                                Instantiate(bulletHit, HitInfo.transform.position, HitInfo.transform.rotation);
                            }
                        }
                    }

                    ShotGunmagazine--;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(ShotGunmagazine, ShotGunmaxMagazine, ShotGunreloading);
                    ShotGunfireTimer = Time.time + ShotGunfireRate;
                }
                else
                {
                    Instantiate(fireSoundEmpty, transform.position, transform.rotation);
                    ShotGunfireTimer = Time.time + ShotGunfireRate;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(ShotGunmagazine, ShotGunmaxMagazine, ShotGunreloading);
                }
            }
        }
    }

    private void Reload()
    {
        if (weaponMode == 1)
        {
            if (Input.GetKey("r") && Riflemagazine != RiflemaxMagazine && !Riflereloading)
            {
                Riflereloading = true;
                RiflereloadTimer = Time.time;
                uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);
                Instantiate(reloadSound, transform.position, transform.rotation);
            }
            if (Riflereloading)
            {
                // animation

                if (Time.time > RiflereloadTimer + RiflereloadCD)
                {
                    Riflemagazine = RiflemaxMagazine;
                    Riflereloading = false;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);

                }

            }
        }
        else if(weaponMode == 2) {
            if (Input.GetKey("r") && ShotGunmagazine != ShotGunmaxMagazine && !ShotGunreloading)
            {
                ShotGunreloading = true;
                ShotGunreloadTimer = Time.time;
                uiController.GetComponent<UIController>().UpdateMagazineBar(ShotGunmagazine, ShotGunmaxMagazine, ShotGunreloading);
                Instantiate(reloadSound, transform.position, transform.rotation);
            }
            if (ShotGunreloading)
            {
                // animation

                if (Time.time > ShotGunreloadTimer + ShotGunreloadCD)
                {
                    ShotGunmagazine = ShotGunmaxMagazine;
                    ShotGunreloading = false;
                    uiController.GetComponent<UIController>().UpdateMagazineBar(ShotGunmagazine, ShotGunmaxMagazine, ShotGunreloading);

                }

            }
        }
    }

    private void SwitchWeaponMode()
    {
        if(weaponMode == 1)
        {
            weaponMode++;
            uiController.GetComponent<UIController>().UpdateMagazineBar(ShotGunmagazine, ShotGunmaxMagazine, ShotGunreloading);
            uiController.GetComponent<UIController>().UpdateWeaponTxt("ShotGun");
        }
        else if (weaponMode == 2)
        {
            weaponMode++;
            uiController.GetComponent<UIController>().UpdateWeaponTxt("RPG");
        }
        else 
        { 
            weaponMode = 1;
            uiController.GetComponent<UIController>().UpdateMagazineBar(Riflemagazine, RiflemaxMagazine, Riflereloading);
            uiController.GetComponent<UIController>().UpdateWeaponTxt("Rifle");
        }
    }
}
