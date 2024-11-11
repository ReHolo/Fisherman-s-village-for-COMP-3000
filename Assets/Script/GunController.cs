using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // 子弹预制体
    public Transform bulletSpawnPoint; // 子弹发射点
    public float bulletSpeed = 20f; // 子弹飞行速度
    public float fireRate = 0.5f; // 射击间隔
    public AudioClip shootSound; // 射击音效

    private float nextFireTime = 0f; // 下次射击时间
    private AudioSource audioSource;

    void Start()
    {
        // 获取 AudioSource 组件
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 检测射击输入
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 更新下次射击时间
        }
    }

    void Shoot()
    {
        // 生成子弹
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;

        // 播放射击音效
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Debug.Log("Fired a bullet!"); // 调试信息
    }
}
