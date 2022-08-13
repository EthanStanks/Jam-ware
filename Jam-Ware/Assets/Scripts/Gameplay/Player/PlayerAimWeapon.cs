using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Transform aimGunEndPointPositionTransform;
    private Transform bulletSpawnTransform;
    private Animator aimAnimator;
    public event EventHandler<OnShootEventArgs> onShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletForce;
    [SerializeField] float shootCooldown;
    private float nextFire;
    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
        aimGunEndPointPositionTransform = aimTransform.Find("GunEndPointPosition");
        bulletSpawnTransform = aimTransform.Find("BulletSpawn");

    }
    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vec.z = 0f;
        return vec;
    }

    void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90) localScale.y = -1f;
        else localScale.y = +1f;
        aimTransform.localScale = localScale;
    }
    void HandleShooting()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFire) // if left mouse click
        {
            nextFire = Time.time + shootCooldown;
            Vector3 mousePosition = GetMouseWorldPosition();
            aimAnimator.SetTrigger("Shoot");
            onShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointPositionTransform.position,
                shootPosition = mousePosition
            });
            GameObject bulletShot = Instantiate(bullet, bulletSpawnTransform.position, bulletSpawnTransform.rotation);
            bulletShot.GetComponent<Rigidbody2D>().AddForce(bulletShot.transform.right * bulletForce);
            Destroy(bulletShot, 2f);
        }
    }
}
