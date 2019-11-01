using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CutScript : MonoBehaviour {
    public Vector2 cutStart;
    public Vector2 cutEnd;
    public Vector2 mousePosition2D;
    List<GameObject> Elements = new List<GameObject>();
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

        Debug.Log("hits length" + hits.Length);
        
        for (int i = 0; i < hits.Length; i++) {
            Elements.Add(hits[i].transform.gameObject);
            
        }
    }

    private void CutElements() {
        Debug.Log("Cutting Elements");
        for (int i = 0; i < this.Elements.Count; i++) {
            
            //var elementPhysics = this.Elements[i].GetComponent<ElementPhysics>();
        }

    }
    
    
}
