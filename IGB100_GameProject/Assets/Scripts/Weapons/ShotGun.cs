using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    Animation animation;
    public GameObject uiController;
    //fire variables
    public float damage = 50.0f;
    public float fireRate = 0;
    private float fireTimer = 0.0f;

    public float BulletSpread;
    [SerializeField] private float BulletRange;
    [SerializeField] private int NumPellets;
    public float magazine = 12;
    public float maxMagazine = 12;
    [SerializeField] private float reloadCD;
    private float reloadTimer;
    public bool reloading = false;


    // fire
    public GameObject muzzleFlash;
    public GameObject bulletHit;
    public GameObject muzzle;
    public GameObject raycastTarget;

    // audio
    public GameObject fireSound;
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
        uiController.GetComponent<UIController>().UpdateMagazineBar(magazine, maxMagazine, reloading);
    }

    // Update is called once per frame
    void Update()
    {
        fireRate = GameObject.FindWithTag("Player").GetComponent<Player>().ShotGunFireRate;
        WeaponFiring();
        Reload();
    }

    private void WeaponFiring()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > fireTimer && !reloading)
        {
            if (magazine > 0) // fire
            {
                Instantiate(fireSound, transform.position, transform.rotation);
                Instantiate(muzzleFlash, muzzle.transform.position, muzzle.transform.rotation);
                animation.Play("Fire");
                Ray[] Pellets = new Ray[NumPellets];
                for(int i = 0; i < Pellets.Length; i++)
                {
                    float spreadX = Random.Range(-BulletSpread, BulletSpread);
                    float spreadY = Random.Range(-BulletSpread, BulletSpread);
                    Vector3 spread = new Vector3(spreadX, spreadY, 0);
                    Pellets[i] = new Ray(muzzle.transform.position, muzzle.transform.forward + spread);
                    Debug.DrawLine(muzzle.transform.position,(muzzle.transform.forward + spread) * BulletRange, Color.yellow, 5.0f);
                    if (Physics.Raycast(Pellets[i], out RaycastHit HitInfo, BulletRange))
                    {
                        if (HitInfo.transform.tag == "Enemy")
                        {
                            HitInfo.transform.GetComponent<Enemy>().TakeDamage(damage);
                            Instantiate(bulletHit, HitInfo.transform.position, HitInfo.transform.rotation);
                        }
                    }
                }

                magazine--;
                uiController.GetComponent<UIController>().UpdateMagazineBar(magazine, maxMagazine, reloading);
                fireTimer = Time.time + fireRate;
            }
            else
            {
                // need reload
                uiController.GetComponent<UIController>().UpdateMagazineBar(magazine, maxMagazine, reloading);
            }
        }
    }

    private void Reload()
    {
        if (Input.GetKey("r") && magazine != maxMagazine && !reloading)
        {
            reloading = true;
            reloadTimer = Time.time;
            uiController.GetComponent<UIController>().UpdateMagazineBar(magazine, maxMagazine, reloading);
        }
        if (reloading)
        {
            // animation

            if (Time.time > reloadTimer + reloadCD)
            {
                magazine = maxMagazine;
                reloading = false;
                uiController.GetComponent<UIController>().UpdateMagazineBar(magazine, maxMagazine, reloading);

            }

        }
    }
}
