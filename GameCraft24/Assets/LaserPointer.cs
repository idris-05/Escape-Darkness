using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    public float _laserBeamLenght;
        private LineRenderer _linerenderer;

    // Start is called before the first frame update
    void Start()
    {
        _linerenderer=GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endposition = transform.position + (transform.right*_laserBeamLenght);
        _linerenderer.SetPositions(new Vector3[]{transform.position, endposition});
    }
}
