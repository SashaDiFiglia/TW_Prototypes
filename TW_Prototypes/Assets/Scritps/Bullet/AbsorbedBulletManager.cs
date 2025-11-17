using System;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbedBulletManager : MonoBehaviour
{
    [SerializeField] private List<Bullet> _player1Bullets;
    [SerializeField] private List<Bullet> _player2Bullets;

    private void Awake()
    {
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
                _player1Bullets.Add(bullet);
                break;
            case PlayerType.PLAYER2:
                _player2Bullets.Add(bullet);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
        }

        bullet.gameObject.SetActive(false);
    }
}