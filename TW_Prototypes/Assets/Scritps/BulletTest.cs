using UnityEngine;

public class BulletTest : MonoBehaviour
{
    [SerializeField] private int Damage;

    [SerializeField] private int _frameInterval;


    private float _frameCount;

    private void Update()
    {
        _frameCount++;

        if (_frameCount % _frameInterval == 0)
        {
            CheckPlayerHit();
        }
    }

    private void CheckPlayerHit()
    {
        var players = Physics.OverlapSphere(transform.position, 0.3f);

        foreach (var player in players)
        {
            if (player.TryGetComponent<AbsorbTest>(out var hit))
            {
                Debug.Log("Player Hit");

                if (hit.TryGetHit(Damage, this))
                {
                    Destroy(gameObject);

                    //vai nella pool
                }
            }
        }
    }
}