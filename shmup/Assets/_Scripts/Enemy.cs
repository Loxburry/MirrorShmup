using System.Collections;          // Required for Arrays & other Collections

using System.Collections.Generic;  // Required for Lists and Dictionaries

using UnityEngine;                 // Required for Unity



public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]

    public float      speed = 10f;      // The speed in m/s

    private float      fireRate = 1.0f;  // Seconds/shot (Unused)
    private float fireTimer = 0.0f;

    public float      health = 10;

    public int        score = 100;      // Points earned for destroying this

    private BoundsCheck bndCheck;                                            // a
    public GameObject       projectilePrefab;

    public float            projectileSpeed = 30;

    



    void Awake() {                                                           // b

        bndCheck = GetComponent<BoundsCheck>();

    }


    // This is a Property: A method that acts like a field

    public Vector3 pos {                                                     // a


        get {

            return( this.transform.position );

        }

        set {

            this.transform.position = value;

        }

    }



    void Update() {

        fireTimer += Time.deltaTime;
        if(fireTimer > fireRate){
            TempFire();
            fireRate = (Random.value * 5f);
            fireTimer = 0f;
        }
        Move();

        if ( bndCheck != null && bndCheck.offDown ) {                    // c

            Destroy( gameObject );

        }

    }

    void TempFire() {                                                        // b

        GameObject projGO = Instantiate<GameObject>( projectilePrefab );

        projGO.transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        //rigidB.velocity = Vector3.down * projectileSpeed;
        rigidB.velocity = new Vector3(Random.Range(-1f, 1f) * 10, -projectileSpeed, 0);
        

    }



    public virtual void Move() {                                             // b

        Vector3 tempPos = pos;

        tempPos.y -= speed * Time.deltaTime;

        pos = tempPos;

    }

    void OnCollisionEnter( Collision coll ) {

      GameObject otherGO = coll.gameObject;                                  // a

      if ( otherGO.tag == "ProjectileEnemy" ) {                               // b

        Destroy( otherGO );        // Destroy the Projectile

        Destroy( this.gameObject );     // Destroy this Enemy GameObject

        } else {

            print( "Enemy hit by non-ProjectileHero: " + otherGO.name );     // c

        }

    }
    

}