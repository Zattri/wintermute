using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Camera cam;
    float bulletImpulse = 15f;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject bullet = Instantiate(bulletPrefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
            bullet.transform.Rotate(180, 0, 0);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(cam.transform.forward * bulletImpulse, ForceMode.Impulse);
        }
    }
}
