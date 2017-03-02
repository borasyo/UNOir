using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    TriangleWave<Vector3> triangleWaveVector3 = null;
    [SerializeField] float move_time = 0.0f;
    [SerializeField] Vector3 minVec3 = Vector3.zero;
    [SerializeField] Vector3 maxVec3 = Vector3.zero;

    TriangleWave<Color> triangleWaveColor = null;
    [SerializeField] float change_time = 0.0f;
    [SerializeField] Color minCol = new Color(1,1,1,0);
    [SerializeField] Color maxCol = new Color(1,1,1,1);

	// Use this for initialization
    void Start () {
        triangleWaveVector3 = RangedTriangleWave.Vector3 (minVec3, maxVec3,  move_time);
        triangleWaveColor   = RangedTriangleWave.Color   (minCol , maxCol , change_time);
	}
	
	// Update is called once per frame
	void Update () {

        triangleWaveVector3.Progress ();
        transform.position = triangleWaveVector3.CurrentValue;

        triangleWaveColor.Progress ();
        GetComponent<SpriteRenderer>().color = triangleWaveColor.CurrentValue;
	}
}
