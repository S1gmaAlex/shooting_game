using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public float range;

    //Gun stats
    public float timeBetweenShooting, reloadTime, spread, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //Recoil
    public float kickbackForce;
    public float kickbackSmooth;
    private Vector2 _currentRotation;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform bulletSpawn;

    //Graphics
    public GameObject bullet;
    public float shootForce;
    public GameObject impactEffect;
    public TextMeshProUGUI ammunitionDisplay;
    public bool allowInvoke = true;
    public ParticleSystem muzzleFlash;

    private void Awake()
    {
        //make sure magazine is full
        
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //Set ammo display
        if (ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft  + " / " + magazineSize );

        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.Lerp(a: transform.localPosition, b: Vector3.zero, t: kickbackSmooth * Time.deltaTime);
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        

        //Reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Reload automatically
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();

        //Shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            
            bulletsShot = 0;
            Shoot();
        }
    }

    
        private void Shoot()
        {
        muzzleFlash.Play();
        transform.localPosition -= new Vector3(x:0 , y:0, z:kickbackForce);
        readyToShoot = false;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log("shooted");
            GameObject bulletHole = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(bulletHole, 0.5f);
        }
        
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(700);

        Vector3 directionWithoutSpread = targetPoint - bulletSpawn.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithoutSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithoutSpread.normalized * shootForce, ForceMode.Impulse);


        bulletsLeft--;
        bulletsShot++;

        //timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);      
        }   
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); 
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
