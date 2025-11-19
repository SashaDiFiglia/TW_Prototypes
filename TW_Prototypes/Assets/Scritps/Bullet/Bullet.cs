using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeSpan;
    [SerializeField] private int _frameInterval;
    [SerializeField] private MeshRenderer _renderer;


    [SerializeField] private Faction _dealsDamageTo;

    public event Action<Bullet> OnHit;

    private float _frameCount;
    private float _lifeTimer;

    private void Update()
    {
        _frameCount++;
        _lifeTimer += Time.deltaTime;

        if (_frameCount % _frameInterval == 0)
        {
            CheckEntityHit();
            return;
        }

        if (_lifeTimer >= _lifeSpan)
        {
            Reset();
        }
    }

    private void CheckEntityHit()
    {
        var entities = Physics.OverlapSphere(transform.position, 0.3f, 1 << (int)_dealsDamageTo);

        foreach (var entity in entities)
        {
            if (entity.TryGetComponent<PlayerParry>(out var hit))
            {
                //Debug.Log("Player Hit");

                if (hit.TryGetHit(_damage, this))
                {
                    OnHit?.Invoke(this);
                    return;
                }
            }

            if (entity.TryGetComponent<BulletSpawner>(out var spawner))
            {
                spawner.TakeDamage();

                OnHit?.Invoke(this);
            }
        }
    }

    public void SetFactionToDealDamage(Faction faction)
    {
        _dealsDamageTo = faction;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void Reset()
    {
        _lifeTimer = 0f;

        _renderer.material.color = Color.gray;

        OnHit?.Invoke(this);
    }
}

public enum Faction
{
    ALLY = 6,
    ENEMY = 3
}