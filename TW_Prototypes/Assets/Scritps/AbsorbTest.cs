using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AbsorbTest : MonoBehaviour
{
    [SerializeField] private int Health;

    [SerializeField] private float _parryWindowTime;
    [SerializeField] private float _parryRadius = 0.5f;

    private Coroutine _parryCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _parryCoroutine == null)
        {
            _parryCoroutine = StartCoroutine(ParryCoroutine());
        }
    }

    public bool TryGetHit(int damage, BulletTest bullet)
    {
        if (_parryCoroutine != null)
        {
            TryAbsorbShot(bullet);

            return false;
        }

        Health -= damage;

        return true;
    }

    private IEnumerator ParryCoroutine()
    {
        Debug.Log("Sto assorbendo colpi");

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

    private void TryAbsorbShot(BulletTest bullet)
    {
        Debug.Log("Absorbing shot");

        Destroy(bullet.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float Yoffset = 0.5f;

        Vector3 OffsetPosition =
            new Vector3(transform.position.x, transform.position.y + Yoffset, transform.position.z);

        Gizmos.DrawSphere(OffsetPosition, _parryRadius);
    }
}