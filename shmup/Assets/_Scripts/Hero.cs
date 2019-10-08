using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class Hero : MonoBehaviour
{

    static public Hero      S; // Singleton                               // a



    [Header("Set in Inspector")]

    // These fields control the movement of the ship

    public float            speed = 30;

    public float            rollMult = -45;

    public float            pitchMult = 30;

    public float            gameRestartDelay = 2f;

    public GameObject       projectilePrefab;

    public float            projectileSpeed = 40;

    //movement of mirror
    private Vector3 mirrorLoc;
    public GameObject mirror;
    public KeyCode mirrorUp;
    public KeyCode mirrorDown;
    public KeyCode mirrorLeft;
    public KeyCode mirrorRight;

    [Header("Set Dynamically")]

    [SerializeField]
 

    private GameObject       lastTriggerGo = null;

    void Awake() {

        if (S == null) {


        S = this; // Set the Singleton                                   // a

    } else {

        Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");

    }

}



void Update () {

    // Pull in information from the Input class

    float xAxis = Input.GetAxis("Horizontal");                            // b

    float yAxis = Input.GetAxis("Vertical");                              // b



    // Change transform.position based on the axes

    Vector3 pos = transform.position;

    pos.x += xAxis * speed * Time.deltaTime;

    pos.y += yAxis * speed * Time.deltaTime;

    transform.position = pos;



    // Rotate the ship to make it feel more dynamic                      // c
    //turning this off for now b/c it's breaking the mirror
    //transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

    //mirror controls
    if(Input.GetKeyDown(mirrorUp)){
        mirrorLoc = new Vector3(pos.x,pos.y + 4,0);
        mirror.transform.position = mirrorLoc;
        mirror.transform.rotation = Quaternion.Euler(0,0,0);
    }
    if(Input.GetKeyDown(mirrorDown)){
        mirrorLoc = new Vector3(pos.x,pos.y - 5,0);
        mirror.transform.position = mirrorLoc;
        mirror.transform.rotation = Quaternion.Euler(0,0,0);
    }
    if(Input.GetKeyDown(mirrorLeft)){
        mirrorLoc = new Vector3(pos.x - 4,pos.y,0);
        mirror.transform.position = mirrorLoc;
        mirror.transform.rotation = Quaternion.Euler(0,0,90);
    }
    if(Input.GetKeyDown(mirrorRight)){
        mirrorLoc = new Vector3(pos.x + 4,pos.y,0);
        mirror.transform.position = mirrorLoc;
        mirror.transform.rotation = Quaternion.Euler(0,0,90);
    }

    //shooting is disabled for now
    // if ( Input.GetKeyDown( KeyCode.Space ) ) {                           // a

    //         TempFire();

    //     }

  }

  void TempFire() {                                                        // b

        GameObject projGO = Instantiate<GameObject>( projectilePrefab );

        projGO.transform.position = transform.position;

        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();

        rigidB.velocity = Vector3.up * projectileSpeed;

    }

  void OnTriggerEnter(Collider other) {

        Transform rootT = other.gameObject.transform.root;

        GameObject go = rootT.gameObject;

        print("Triggered: "+go.name);

        if (go == lastTriggerGo) {                                           // c

            return;

        }

        lastTriggerGo = go;                                                  // d



        if (go.tag == "Enemy" || go.tag == "ProjectileEnemy") {  // If the shield was triggered by an enemy

            Destroy(go);          // … and Destroy the enemy                 // e
            Destroy(this.gameObject);
            Main.S.DelayedRestart( gameRestartDelay );

        } else {

            print("Triggered by non-Enemy: "+go.name);                       // f

        }

    }

}