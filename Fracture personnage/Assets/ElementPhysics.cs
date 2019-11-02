using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using Vector2 = UnityEngine.Vector2;

public class ElementPhysics : MonoBehaviour {
    public Rigidbody2D[] neighbors = new Rigidbody2D[4];
    public Rigidbody2D[] jointsRigidbodys = new Rigidbody2D[4];
    public FixedJoint2D[] joints = new FixedJoint2D[4];

    private Collider2D coll;
    private Rigidbody2D rb;
    private Vector2 extents;
    private float horizontalInput;
    private float verticalInput;
    private Vector2 velocity;
    public float force = 3f;
    

    public float vitesseDeplacement = 10f;

    private void Awake() {
        GetNeighboursRb();
        CreateJoints();
        coll = this.GetComponent<Collider2D>();
        extents = this.coll.bounds.extents;
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update() {
        this.horizontalInput = Input.GetAxisRaw("Horizontal");
        this.verticalInput = Input.GetAxisRaw("Vertical");
        this.velocity = new Vector2(this.horizontalInput, this.verticalInput);
    }

    private void FixedUpdate() {
        Debug.Log(this.velocity);
        this.rb.AddForce(this.velocity * this.force * Time.fixedDeltaTime, ForceMode2D.Force);
    }
    
    
    private void GetNeighboursRb() {
        var dir = new Vector2();
        for (int i = 0; i < this.neighbors.Length; i++) {
            switch (i) {
                case 0:
                    dir = Vector2.right;
                    break;
                case 1:
                    dir = Vector2.down;
                    break;
                case 2:
                    dir = Vector2.left;
                    break;
                case 3:
                    dir = Vector2.up;
                    break;
            }

            var hit = Physics2D.RaycastAll(transform.position, dir, dir.magnitude);
            foreach (var t in hit) {
                if (t.transform.CompareTag("Element")){
                    if (t.transform.gameObject != this.gameObject) {
                        this.neighbors[i] = t.rigidbody;
                    }
                }
            }
        }
    }
    private void CreateJoints() {
        for (int i = 0; i < this.neighbors.Length; i++) {
            if (this.neighbors[i] != null) {
                this.joints[i] = this.gameObject.AddComponent<FixedJoint2D>();
                
                this.joints[i].connectedBody = this.neighbors[i];
                this.jointsRigidbodys[i] = this.joints[i].connectedBody;
            }
        }
    }
}
