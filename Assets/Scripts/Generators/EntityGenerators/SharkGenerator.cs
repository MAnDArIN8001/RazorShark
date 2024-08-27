using System.Collections;
using UnityEngine;

public class SharkGenerator : EntityGenerator<GameObject>
{
    [SerializeField] private float _minRealodTime;
    [SerializeField] private float _maxRealodTime;

    [SerializeField] private Transform _instancePoint;
    [SerializeField] private Transform[] _routePoints;

    private SharkHealth _sharkHelath;

    private void Awake()
    {
        Generate();
    }

    protected override void Generate()
    {
        GameObject sharkInstance = Instantiate(_entityPrefab, _instancePoint.position, Quaternion.identity);

        _sharkHelath = sharkInstance.GetComponent<SharkHealth>();
        _sharkHelath.OnDied += HandleLastCrabDeath;

        sharkInstance.GetComponent<SharkMover>().Initialize(_routePoints);
    }

    private void HandleLastCrabDeath()
    {
        _sharkHelath.OnDied -= HandleLastCrabDeath;
        _sharkHelath = null;

        StartCoroutine(ReloadGenerator());
    }

    private IEnumerator ReloadGenerator()
    {
        float randomReloadTime = UnityEngine.Random.Range(_minRealodTime, _maxRealodTime);

        yield return new WaitForSeconds(randomReloadTime);

        Generate();
    }
}
