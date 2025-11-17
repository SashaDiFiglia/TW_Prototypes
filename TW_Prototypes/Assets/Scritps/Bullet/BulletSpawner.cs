using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float _spawnInterval;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _spawnInterval)
        {
            _timer = 0;

            ShootBullet();

            //Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    private void ShootBullet()
    {
        var bullet = BulletPool.Instance.TryGetBullet();

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
    }
}