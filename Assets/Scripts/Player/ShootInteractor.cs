using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInteractor : Interactor
{
    [SerializeField] private Input inputType;

    [Header("Gun")]
    public MeshRenderer gunRender;
    public Color bulletColor;
    public Color rocketColor;
    
    [Header("Shooting")]
    
     public ObjectPool bulletPool;
     public ObjectPool RocketPool;
    
    
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float shootForce;
    [SerializeField] private PlayerMovementBehaviour movementBehaviour;

    private float finalShootVelocity;
    private IShootStrategy currentShootStrategy;


    /*public enum Input
    {
        Primary,
        Secondary
    }*/

    public override void Interact()
    {
        if(currentShootStrategy == null)
        {
            currentShootStrategy = new BulletStrategy(this);
        }

        if(playerInput.weapon1Pressed)
        {
            currentShootStrategy = new BulletStrategy(this);
        }
        if(playerInput.weapon2Pressed)
        {
            currentShootStrategy = new RocketStrategy(this);
        }
        if(playerInput.primaryShootPressed && currentShootStrategy != null) 
        {
            currentShootStrategy.Shoot();   
        }
    }


    void Shoot()
    {
        finalShootVelocity = movementBehaviour.GetForwardSpeed() + shootForce;
        PooledObject pooledBullet = bulletPool.GetPooledObject();
        pooledBullet.gameObject.SetActive(true);
        
        //Rigidbody bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        
        Rigidbody bullet = pooledBullet.GetComponent<Rigidbody>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;

        bullet.velocity = shootPoint.forward * finalShootVelocity;

        //Destroy(bullet.gameObject, 5f);

        bulletPool.DestroyPooledObject(pooledBullet, 5.0f);
    }
    
    public float GetShootVelocity()
    {
        finalShootVelocity = movementBehaviour.GetForwardSpeed() + shootForce;
        return finalShootVelocity;
    }
    public Transform GetShootPoint()
    {
        return shootPoint;
    }

}
