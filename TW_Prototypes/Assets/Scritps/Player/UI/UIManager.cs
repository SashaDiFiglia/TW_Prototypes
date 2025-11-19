using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerType _playerType;

    [SerializeField] private TextMeshProUGUI _bulletAmountText;

    private int bulletAmount;

    private void Start()
    {
        var players = FindObjectsByType<PlayerParry>(default);

        foreach (var player in players)
        {
            if (player.GetPlayerType() == _playerType)
            {
                if (player.TryGetComponent<PlayerShoot>(out var shoot))
                {
                    shoot.OnShoot += RemoveBullet;
                }

                continue;
            }

            player.OnUIUpdate += AddBullet;
        }
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    private void AddBullet()
    {
        bulletAmount++;

        _bulletAmountText.text = bulletAmount.ToString();
    }

    private void RemoveBullet()
    {
        bulletAmount--;

        _bulletAmountText.text = bulletAmount.ToString();
    }
}