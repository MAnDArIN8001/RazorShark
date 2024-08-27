using System.Collections;
using UnityEngine;

public class SkyGenerator : MonoBehaviour
{
    [SerializeField] private float _maxGeneratorReloadTime;
    [SerializeField] private float _minGeneratorReloadTime;
    [SerializeField] private float _maxYOffset;
    [SerializeField] private float _minYOffset;

    [SerializeField] private Transform[] _instancePoints;

    [SerializeField] private GameObject[] _skyPrefabs;

    private void Awake()
    {
        GenerateSky();
    }

    private void GenerateSky()
    {
        GameObject skyPrefab = GetRandomItem(_skyPrefabs);
        Transform instancePoint = GetRandomItem(_instancePoints);

        Vector2 direction = GetDirectionToClothestInstancePoint(instancePoint);
        Vector3 offset = new Vector3(0, Random.Range(_minYOffset, _maxYOffset), 0);

        GameObject skyInstance = Instantiate(skyPrefab, instancePoint.position + offset, Quaternion.identity, transform);
        skyInstance.GetComponent<SkyMover>().Initialize(direction);

        StartCoroutine(ReloadGenerator());
    }

    private Vector3 GetDirectionToClothestInstancePoint(Transform instancePoint)
    {
        Transform anotherPoint = instancePoint;

        foreach (var item in _instancePoints)
        {
            if (item != instancePoint)
            {
                anotherPoint = item;

                break;
            }
        }

        return anotherPoint.position - instancePoint.position;
    }

    private T GetRandomItem<T>(T[] array)
    {
        int randomSkyIndex = Random.Range(0, array.Length);

        return array[randomSkyIndex];
    }

    private IEnumerator ReloadGenerator()
    {
        float randomReloadTime = Random.Range(_minGeneratorReloadTime, _maxGeneratorReloadTime);

        yield return new WaitForSeconds(randomReloadTime);

        GenerateSky();
    }
}
