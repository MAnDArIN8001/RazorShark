using System.Collections;
using UnityEngine;

public class CrabGenerator : EntityGenerator<GameObject>
{
    [SerializeField] private float _minRealodTime;
    [SerializeField] private float _maxRealodTime;
    
    [SerializeField] private Transform _instancePoint;
    [SerializeField] private Transform[] _routePoints;

    private CrabHealth _crabHealth;

    private void Awake()
    {
        Generate();
    }

    protected override void Generate()
    {
        GameObject crabInstance = Instantiate(_entityPrefab, _instancePoint.position, Quaternion.identity);

        _crabHealth = crabInstance.GetComponent<CrabHealth>();
        _crabHealth.OnDied += HandleLastCrabDeath;

        crabInstance.GetComponent<CrabMover>().Initialize(_routePoints);
    }

    private void HandleLastCrabDeath()
    {
        _crabHealth.OnDied -= HandleLastCrabDeath;
        _crabHealth = null;

        StartCoroutine(ReloadGenerator());
    }

    private IEnumerator ReloadGenerator()
    {
        float randomReloadTime = UnityEngine.Random.Range(_minRealodTime, _maxRealodTime);

        yield return new WaitForSeconds(randomReloadTime);

        Generate();
    }
}
