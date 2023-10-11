using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStrategy : IShootStrategy
{
    ShootInteractor _interactor;
    Transform shootPoint;
    public RocketStrategy(ShootInteractor interactor) 
    {
        Debug.Log("Swithced to rocket");

        _interactor = interactor;
        shootPoint = interactor.GetShootPoint();
        _interactor.gunRender.material.color = _interactor.rocketColor;
    }

  public void Shoot()
    {
        PooledObject pooledBullet = _interactor.RocketPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);

        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * _interactor.GetShootVelocity();

        //Destroy(bullet.gameObject, 5f);

        _interactor.RocketPool.DestroyPooledObject(pooledBullet, 5.0f);
    }
}
