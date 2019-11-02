using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CutScript : MonoBehaviour {
    public Vector2 cutStart;
    public Vector2 cutEnd;
    public Vector2 mousePosition2D;
    List<Rigidbody2D> Elements = new List<Rigidbody2D>();
    private Camera cam;

    private void Start() {
        cam = Camera.main;
    }

    private void Update() {
        this.Slice();


        //Debug.Log(this.cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, this.cam.nearClipPlane)));
    }

    private void Slice() {
        this.mousePosition2D = this.cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            cutStart = mousePosition2D;
            
        }

        if (Input.GetMouseButton(0)) {
            Debug.DrawLine(cutStart, mousePosition2D, Color.magenta);
            RegisterElements();
        }

        if (Input.GetMouseButtonUp(0)) {
            cutEnd = Input.mousePosition;
            CutElements();
            cutStart = Vector2.zero;
            cutEnd = Vector2.zero;
        }
    }

    private void RegisterElements() {
        Debug.Log("Registering Elements");
        var distance = Vector2.Distance(this.cutStart, this.mousePosition2D);
        RaycastHit2D[] hits = Physics2D.RaycastAll(this.cutStart, this.mousePosition2D - this.cutStart, distance);
        //Debug.Log("hits length" + hits.Length);
        
        for (int i = 0; i < hits.Length; i++) {
            if(hits[i].transform.CompareTag("Element")) this.Elements.Add(hits[i].transform.GetComponent<Rigidbody2D>());
        }
    }

    private void CutElements() { 
        int destroyed = 0;
        Debug.Log("Cutting Elements");
        /*Pour chaque élément, compare chaque voisin avec chaque élément dans la liste
        *S'ils sont identiques, !alors détruire le joint qui relie l'élément et le voisin!
        *Ensuite, vider l'index de l'array de joint
        *Finalement vider la liste
        */

        for (int i = 0; i < this.Elements.Count; i++) {
            ElementPhysics elementPhysics = this.Elements[i].GetComponent<ElementPhysics>();
            
            for (int j = 0; j < elementPhysics.neighbors.Length; j++) {
                if(this.Elements.Contains(elementPhysics.neighbors[j]))
                    for (int k = 0; k < elementPhysics.joints.Length; k++) {
                        Destroy(elementPhysics.joints[k]);
                    }
                //elementPhysics.joints[j].attachedRigidbody
            }
        }
    }
        

        
        
        
        
        
        
//        //Pour chaque élément enregistré par la coupe
//        for (int i = 0; i < this.Elements.Count; i++) { 
//            //Récupérer son script d'élément
//            var elementPhysics = this.Elements[i].GetComponent<ElementPhysics>(); 
//            
//            //Comparer chaque élément avec un de 
//            
//            
//            for (int j = 0; j < elementPhysics.jointsRigidbodys.Length; j++) { 
//                
//                
//                if (this.Elements[i] != null) {
//                    
//                    // détruire les joints pour chaque rigidbody de neighbor
//                    if (this.Elements[i].GetComponent<Rigidbody2D>() == elementPhysics.jointsRigidbodys[j]) {
//                        Destroy(elementPhysics.joints[j]);
//                        elementPhysics.joints[j] = null;
//                        elementPhysics.jointsRigidbodys[j] = null; 
//                        destroyed += 1;
//                        print("destroyed : " + destroyed);
//                    }
//                }
//        }
        
    }
