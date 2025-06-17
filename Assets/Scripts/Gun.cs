using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Set References")]
    [SerializeField] protected ParticleSystem fireFlash;
    [SerializeField] protected Transform shellPoint;
    [SerializeField] protected Transform muzzlePoint;
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected GameObject bulletShell;
    [SerializeField] protected AudioSource tickSound;
    [SerializeField] protected AudioSource fireSound;

    [Header("Set Values")]
    [Range(0, 5)][SerializeField] protected float fireDelay;
    [Range(100, 10000)][SerializeField] protected float fireSpeed;

    [Header("-----Debug")]
    public bool isLoaded;
    public Coroutine fireRoutine;
    public MagazineController magazineController;

    void Start()
    {
        isLoaded = false;
    }

    public void magazineLoad(MagazineController controller)
    {
        this.magazineController = controller;
        isLoaded = true;
    }

    public void magazineUnLoad()
    {
        magazineController = null;
        isLoaded = false;
    }

    public void Fire()
    {
        if (isLoaded && fireRoutine == null)
        {
            if (magazineController.CurrentBullet > 0)
            {
                fireRoutine = StartCoroutine(FireRoutine());
                magazineController.CurrentBullet--;
            }
            else
            {
                tickSound.Play();
            }
        }
    }

    IEnumerator FireRoutine()
    {
        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody bulletRig = bullet.GetComponent<Rigidbody>();
        bulletRig.collisionDetectionMode = CollisionDetectionMode.Continuous;
        bulletRig.AddForce(muzzlePoint.forward * fireSpeed, ForceMode.Impulse);
        
        GameObject bulletShellOut = Instantiate(bulletShell, shellPoint.position, shellPoint.rotation);
        Rigidbody bulletShellRig = bulletShellOut.GetComponent<Rigidbody>();
        bulletShellRig.AddForce(shellPoint.forward * 5f, ForceMode.Impulse);
        Destroy(bulletShellOut, 3f);
       
        Instantiate(fireFlash, muzzlePoint.position, muzzlePoint.rotation);
        
        fireSound.Play();

        yield return new WaitForSeconds(fireDelay);
        
        if(bullet != null)
        {
            Destroy(bullet);
        }
        fireRoutine = null;
    }
}