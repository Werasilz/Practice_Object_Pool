using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DroneObjectPool : MonoBehaviour
{
    public int maxPoolSize = 10;
    public int stackDefaultCapacity = 10;

    private IObjectPool<Drone> _pool;

    public IObjectPool<Drone> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<Drone>(CreatedPooledItem,
                                                OnTakeFromPool,
                                                OnReturnToPool,
                                                OnDestroyPoolObject,
                                                true,
                                                stackDefaultCapacity,
                                                maxPoolSize);
            }

            return _pool;
        }
    }

    private Drone CreatedPooledItem()
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

        Drone drone = go.AddComponent<Drone>();

        go.name = "Drone";
        drone.Pool = Pool;

        return drone;
    }

    private void OnTakeFromPool(Drone drone)
    {
        drone.gameObject.SetActive(true);
    }

    private void OnReturnToPool(Drone drone)
    {
        drone.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Drone drone)
    {
        Destroy(drone.gameObject);
    }

    public void Spawn()
    {
        var amount = UnityEngine.Random.Range(1, 10);

        for (int i = 0; i < amount; i++)
        {
            var drone = Pool.Get();

            drone.transform.position = UnityEngine.Random.insideUnitSphere * 10f;
        }
    }
}
