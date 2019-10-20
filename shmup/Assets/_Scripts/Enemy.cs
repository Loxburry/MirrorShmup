using System.Collections;          // Required for Arrays & other Collections

using System.Collections.Generic;  // Required for Lists and Dictionaries

using UnityEngine;                 // Required for Unity



public class Enemy : MonoBehaviour {

    [Header("Set in Inspector: Enemy")]

    public float      speed = 10f;      // The speed in m/s

    private float      fireRate = 1.0f;  // Seconds/shot (Unused)
    private float fireTimer = 0.0f;

    public float      health = 10;

    public int        pointVal = 10;      // Points earned for destroying this

    protected BoundsCheck bndCheck;                                            // a
    public GameObject       projectilePrefab;

    public float            projectileSpeed = 30;

    public int bulletOffset;
    private int bulletDirection = 1;

    



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

        if(GameObject.Find("_Hero")){
            var posH = GameObject.Find("_Hero").transform.position;
            if(posH.y > transform.position.y){
                bulletDirection = 1;
            }
            else{
                bulletDirection = -1;
            }
        }
        
        
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

        if(GameObject.Find("_Hero")){
            GameObject projGO = Instantiate<GameObject>( projectilePrefab );

            bulletOffset = bulletOffset * bulletDirection;
            projGO.transform.position = new Vector3(transform.position.x, transform.position.y + bulletOffset, transform.position.z);

            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

            var heroP = GameObject.Find("_Hero").transform.position;
            // bulletDirection should only be set to 1 or -1: 1 shoots up, -1 shoots down
            rigidB.velocity = new Vector3(heroP.x, bulletDirection * projectileSpeed, 0);
        }
    }



    public virtual void Move() {                                             // b

        Vector3 tempPos = pos;

        tempPos.y -= speed * Time.deltaTime;

        pos = tempPos;

    }

    void OnCollisionEnter( Collision coll ) {

      GameObject otherGO = coll.gameObject;                                  // a

      if ( otherGO.tag == "ProjectileEnemy" ) {                               // b

        scoreScript.S.UpdateScore(pointVal);

        Destroy( otherGO );        // Destroy the Projectile

        Destroy( this.gameObject );     // Destroy this Enemy GameObject

        } else {

            print( "Enemy hit by " + otherGO.name );     // c

        }

    }
    

}