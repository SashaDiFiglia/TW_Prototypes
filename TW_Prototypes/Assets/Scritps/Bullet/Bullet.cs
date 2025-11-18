using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeSpan;
    [SerializeField] private int _frameInterval;

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
            CheckPlayerHit();
            return;
        }

        if (_lifeTimer >= _lifeSpan)
        {
            Reset();
        }
    }

    private void CheckPlayerHit()
    {
        var players = Physics.OverlapSphere(transform.position, 0.3f, 1 << (int)_dealsDamageTo);

        foreach (var player in players)
        {
            if (player.TryGetComponent<PlayerParry>(out var hit))
            {
                Debug.Log("Player Hit");

                if (hit.TryGetHit(_damage, this))
                {
                    OnHit?.Invoke(this);
                }
            }
        }
    }

    public void SetFactionToDealDamage(Faction faction)
    {
        _dealsDamageTo = faction;
    }

    private void Reset()
    {
        _lifeTimer = 0f;

        OnHit?.Invoke(this);
    }
}

public enum Faction
{
    ALLY = 6,
    ENEMY = 3
}