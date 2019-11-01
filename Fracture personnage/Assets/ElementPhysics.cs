using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementPhysics : MonoBehaviour {
    public Rigidbody2D[] neighbors = new Rigidbody2D[4];
    public List<Rigidbody2D> jointsRigidbodys;
    public FixedJoint2D[] joints = new FixedJoint2D[4];

    private Collider2D coll;
    private Vector2 extents;

    private void Awake() {
        GetNeighboursRb();
        CreateJoints();
        coll = this.GetComponent<Collider2D>();
        extents = this.coll.bounds.extents;
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
            Debug.Log(hit.Length);
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
                this.joints[i] = gameObject.AddComponent<FixedJoint2D>();
                this.joints[i].connectedBody = this.neighbors[i];
            }
        }
    }
}
