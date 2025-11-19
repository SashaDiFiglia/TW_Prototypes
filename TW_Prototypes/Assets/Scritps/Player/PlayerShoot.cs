using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private PlayerType playerType;

    public event Action OnShoot;

    private void Awake()
    {
        var playerParry = GetComponent<PlayerParry>();

        playerType = playerParry.GetPlayerType();
    }

    public void CallShoot(InputAction.CallbackContext obj)
    {
        var bullet = AbsorbedBulletManager.Instance.TryGiveBullet(playerType);

        if (bullet == null)
        {
            return;
        }

        OnShoot?.Invoke();

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.SetFactionToDealDamage(Faction.ENEMY);

        bullet.SetColor(Color.white);

        bullet.gameObject.SetActive(true);
    }
}