using UnityEngine;

public class SpawnData : MonoBehaviour
{
    [SerializeField] private GameObject _businessPrefab;
    [SerializeField] private Transform _businessTransform;
    [SerializeField] private GameObject _balancePrefab;
    [SerializeField] private Transform _balanceTransform;

    public GameObject BusinessPrefab => _businessPrefab;
    public Transform BusinessTransform => _businessTransform;
    public GameObject BalancePrefab => _balancePrefab;
    public Transform BalanceTransform => _balanceTransform;
}