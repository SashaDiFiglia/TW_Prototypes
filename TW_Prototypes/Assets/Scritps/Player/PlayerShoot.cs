using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    private PlayerType playerType;

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

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.SetFactionToDealDamage(Faction.ENEMY);

        bullet.gameObject.SetActive(true);
    }
}