using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    [SerializeField] private int _initialBullets;

    [SerializeField] private GameObject _bulletPrefab;

    private List<Bullet> _bulletsPool;

    private void Awake()
    {
        if (!TryRegisterToInstance())
        {
            return;
        }

        _bulletsPool = new List<Bullet>();

        for (int i = 0; i < _initialBullets; i++)
        {
            var bullet = Instantiate(_bulletPrefab, transform);

            var bulletComponent = bullet.GetComponent<Bullet>();

            _bulletsPool.Add(bulletComponent);

            bulletComponent.OnHit += TurnOffBullet;

            bullet.gameObject.SetActive(false);
        }
    }

    private bool TryRegisterToInstance()
    {
        if (Instance == null)
        {
            Instance = this;
            return true;
        }

        Destroy(this);
        return false;
    }

    public Bullet TryGetBullet()
    {
        if (_bulletsPool.Count <= 0)
        {
            return null;
        }

        var bullet = _bulletsPool[0];

        bullet.gameObject.SetActive(true);

        _bulletsPool.RemoveAt(0);

        return bullet;
    }

    private void TurnOffBullet(Bullet bullet)
    {
        _bulletsPool.Add(bullet);

        bullet.transform.position = transform.position;

        bullet.transform.rotation = transform.rotation;

        bullet.gameObject.SetActive(false);
    }
}