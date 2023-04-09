using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinFactory : BaseFactory<Coin>
{
    [SerializeField] private Transform _targetTranform;
    [SerializeField] private float _distance;
    [SerializeField] private int _maxActiveObjectsCount;
    
    [Header("Bounds")]
    [SerializeField] private float _minX;
    [SerializeField] private float _maxX;
    [SerializeField] private float _offsetY;

    private Queue<Coin> _activeObjects;
    private Vector3 _previousPosition;

    private void Awake()
    {
        _activeObjects = new Queue<Coin>(_maxActiveObjectsCount);
    }

    protected override void Start()
    {
        base.Start();
        _previousPosition = _targetTranform.position;
    }
    
    private void Update()
    {
        if (_targetTranform.position.y - _previousPosition.y > _distance)
        {
            _previousPosition = _targetTranform.position;
            SpawnCoin(RandomizePosition());
        }
    }

    private void SpawnCoin(Vector3 position)
    {
        Coin coin = FactoryObjects.Get();
        coin.transform.position = position;

        if (_activeObjects.Count >= _maxActiveObjectsCount)
            _activeObjects.Dequeue().gameObject.SetActive(false);
        
        _activeObjects.Enqueue(coin);
    }

    private Vector3 RandomizePosition()
    {
        float y = _targetTranform.position.y + _offsetY;
        float x = Random.Range(_minX, _maxX);
        return new Vector3(x, y);
    }
}
