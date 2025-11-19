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

    [SerializeField] private ParticleSystem _hitEffect;


    private Coroutine parryCoroutine;

    public event Action<PlayerType, Bullet> OnBulletAbsorbed;
    public event Action OnUIUpdate;


    public bool TryGetHit(int damage, Bullet bullet)
    {
        if (parryCoroutine != null)
        {
            ParryBullet(bullet);

            return false;
        }

        _hitEffect.Emit(5);

        Health -= damage;

        return true;
    }

    public void CallParryCoroutine(InputAction.CallbackContext obj)
    {
        if (parryCoroutine != null)
        {
            return;
        }

        parryCoroutine = StartCoroutine(ParryCoroutine());
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

        parryCoroutine = null;

        _shieldRenderer.enabled = false;

        //Debug.Log("Finito di assorbire");

        yield return null;
    }

    private void ParryBullet(Bullet bullet)
    {
        //Debug.Log("Absorbing shot");

        OnBulletAbsorbed?.Invoke(_playerType, bullet);

        OnUIUpdate?.Invoke();

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