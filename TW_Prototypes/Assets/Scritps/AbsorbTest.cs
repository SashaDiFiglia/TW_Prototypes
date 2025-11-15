using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbsorbTest : MonoBehaviour
{
    [SerializeField] private int Health;

    [SerializeField] private float _parryWindowTime;

    private Coroutine _parryCoroutine;
    

    public bool TryGetHit(int damage, BulletTest bullet)
    {
        if (_parryCoroutine != null)
        {
            AbsorbShot(bullet);

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
        Debug.Log(name + ": Sto assorbendo colpi");

        float timer = 0;

        while (timer <= _parryWindowTime)
        {
            timer += Time.deltaTime;

            yield return null;
        }

        _parryCoroutine = null;

        Debug.Log("Finito di assorbire");

        yield return null;
    }

    private void AbsorbShot(BulletTest bullet)
    {
        Debug.Log("Absorbing shot");

        Destroy(bullet.gameObject);
    }
}