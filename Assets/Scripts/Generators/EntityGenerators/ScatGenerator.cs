using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatGenerator : EntityGenerator<GameObject>
{
    [SerializeField] private float _minRealodTime;
    [SerializeField] private float _maxRealodTime;

    [SerializeField] private Transform _instancePoint;
    [SerializeField] private Transform[] _routePoints;

    private ScatHealth _sharkHelath;

    private void Awake()
    {
        Generate();
    }

    protected override void Generate()
    {
        GameObject sharkInstance = Instantiate(_entityPrefab, _instancePoint.position, Quaternion.identity);

        _sharkHelath = sharkInstance.GetComponent<ScatHealth>();
        _sharkHelath.OnDied += HandleLastCrabDeath;

        sharkInstance.GetComponent<ScatMover>().Initialize(_routePoints);
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
