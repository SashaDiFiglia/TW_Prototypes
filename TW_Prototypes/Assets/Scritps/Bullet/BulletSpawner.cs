using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [Header("Rotation Settings")] 
    [SerializeField] private float _rotationAmount;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float _spawnInterval;

    private float timer;
    private float startX;
    private float targetAngle;

    private void Start()
    {
        startX = transform.eulerAngles.x;
    }

    private void Update()
    {
        Rotate();

        timer += Time.deltaTime;

        if (timer >= _spawnInterval)
        {
            timer = 0;

            ShootBullet();

            //Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    private void Rotate()
    {
        targetAngle = startX + Mathf.Sin(Time.time * _rotationSpeed) * _rotationAmount;
        transform.rotation = Quaternion.Euler(targetAngle, 90, 90);
    }

    public void TakeDamage()
    {
        Debug.Log(name + " Took damage");
    }

    private void ShootBullet()
    {
        var bullet = BulletPool.Instance.TryGetBullet();

        bullet.SetFactionToDealDamage(Faction.ALLY);

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.SetColor(Color.red);

        bullet.gameObject.SetActive(true);
    }
}