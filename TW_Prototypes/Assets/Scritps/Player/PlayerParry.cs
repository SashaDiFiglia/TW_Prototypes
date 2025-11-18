using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParry : MonoBehaviour
{
    [SerializeField] private int Health;

    [SerializeField] private float _parryWindowTime;

    [SerializeField] private PlayerType _playerType;

    [SerializeField] private MeshRenderer _shieldRenderer;


    private Coroutine _parryCoroutine;

    public event Action<PlayerType, Bullet> OnBulletAbsorbed;


    public bool TryGetHit(int damage, Bullet bullet)
    {
        if (_parryCoroutine != null)
        {
            ParryBullet(bullet);

            return false;
        }

        Health -= damage;

        return true;
    }

    public void CallParryCoroutine(InputAction.CallbackContext obj)
    {
        _parryCoroutine = StartCoroutine(ParryCoroutine());
    }

    private IEnumerator ParryCoroutine()
    {
        //Debug.Log(name + ": Sto assorbendo colpi");

        _shieldRenderer.enabled = true;

        float timer = 0;

        while (timer <= _parryWindowTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        _parryCoroutine = null;

        _shieldRenderer.enabled = false;

        //Debug.Log("Finito di assorbire");

        yield return null;
    }

    private void ParryBullet(Bullet bullet)
    {
        Debug.Log("Absorbing shot");

        OnBulletAbsorbed?.Invoke(_playerType, bullet);

        //Destroy(bullet.gameObject);
    }

    public PlayerType GetPlayerType()
    {
        return _playerType;
    }
}

public enum PlayerType
{
    PLAYER1,
    PLAYER2
}