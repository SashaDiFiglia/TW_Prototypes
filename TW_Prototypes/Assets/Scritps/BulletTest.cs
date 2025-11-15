using UnityEngine;

public class BulletTest : MonoBehaviour
{
    [SerializeField] private int Damage;

    private void Update()
    {
        var player = Physics.OverlapSphere(transform.position, 0.5f);

        foreach (var item in player)
        {
            if (item.TryGetComponent<AbsorbTest>(out var hit))
            {
                Debug.Log("Player Colpito");

                if (hit.TryGetHit(Damage, this))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}