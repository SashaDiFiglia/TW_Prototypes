using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbsorbedBulletManager : MonoBehaviour
{
    [SerializeField] private List<Bullet> _player1Bullets;
    [SerializeField] private List<Bullet> _player2Bullets;

    public static AbsorbedBulletManager Instance { get; private set; }

    private void Awake()
    {
        if (!TryRegisterToInstance())
        {
            return;
        }

        var players = FindObjectsByType<PlayerParry>(default);

        if (players != null)
        {
            foreach (var player in players)
            {
                player.OnBulletAbsorbed += AddBullet;
            }
        }
    }

    private void AddBullet(PlayerType playerType, Bullet bullet)
    {
        switch (playerType)
        {
            case PlayerType.PLAYER1:
                _player1Bullets.Insert(0, bullet);
                break;
            case PlayerType.PLAYER2:
                _player2Bullets.Insert(0, bullet);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
        }

        bullet.gameObject.SetActive(false);
    }

    public Bullet TryGiveBullet(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.PLAYER1:

                if (_player2Bullets.Any())
                {
                    var bullet = _player2Bullets.First();

                    _player2Bullets.Remove(bullet);

                    return bullet;
                }

                break;
            case PlayerType.PLAYER2:

                if (_player1Bullets.Any())
                {
                    var bullet = _player1Bullets.First();

                    _player1Bullets.Remove(bullet);

                    return bullet;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
        }

        return null;
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
}