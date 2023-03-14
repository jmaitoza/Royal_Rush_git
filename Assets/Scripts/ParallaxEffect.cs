using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Original from AdamCYounis parallax tutorial

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget; //follow the player
    
    // Starting pos for the parallax game obj
    Vector2 startingPos;
    
    // Start Z value of the parallax game obj
    float startingZ;

    // => operator means this will update on every frame, despite not being in the Update() method
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPos; // dist that the cam has moved from the starting pos of the parallax obj

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    // if obj is in front of target, use near clip plane. if behind target, use far clip plane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    
    // the further the obj from the player, the faster the ParallaxEffect obj will move. Drag its Z val closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    { 
        // when the target moves, move the parallax obj the same distance times a multiplier
        Vector2 newPos = startingPos + camMoveSinceStart * parallaxFactor;

        // the X/Y pos changes based on target travel speed times the parallax factor, but Z stays constant
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}
