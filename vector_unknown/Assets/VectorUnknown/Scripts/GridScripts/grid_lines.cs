using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid_lines : MonoBehaviour {

	public float line_width = 1.0f;
	public float width = 40f, height = 40f;

	public Vector3 top, bot;
	public Material line_material;
	public Vector3[] lines_start, lines_end;
	public List< GameObject> lines;

	void Start () {
		GameObject grid_lines = new GameObject ();
		grid_lines.name = "grid_lines";

		top = new Vector3( 0, 0, width);
		bot = new Vector3( 0, 0, -width);

		line_width = 0.25f;
		lines = new List<GameObject> ();
		lines_start = new Vector3[(int)width + 1];
		lines_end = new Vector3[ (int)width + 1];
		for (int i = 0; i < width; i++){
			lines_start[ i] = new Vector3( -width/2  , 0, i -width/2);
			lines_end[i] = new Vector3( width/2f, 0, i -width/2);
			lines.Add( gen_line_render( lines_start[i], lines_end[i]));
		//}

		//for (int i = 0; i < 11; i++){
			lines_start[ i] = new Vector3( i-width/2f, 0,  -width/2f);
			lines_end[i] = new Vector3( i-width/2f, 0, width/2f);
			lines.Add( gen_line_render( lines_start[i], lines_end[i]));
		}

		foreach (GameObject go in lines)
			go.transform.SetParent (grid_lines.transform);
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.S) && !Input.GetKey( KeyCode.LeftShift)) {
			line_width += .5f;
		}
		if (Input.GetKeyDown (KeyCode.S) && Input.GetKey( KeyCode.LeftShift)) {
			line_width -= .5f;
		}

		foreach (GameObject line in lines) {
			line.GetComponent< LineRenderer> ().startWidth = line_width;
			line.GetComponent< LineRenderer> ().endWidth = line_width;
			line.GetComponent< LineRenderer> ().material = line_material;
		}
	}

	GameObject gen_line_render( Vector3 start, Vector3 end){
		GameObject line_to_render = new GameObject ();
		line_to_render.name = "Grid Line";
		//line_to_render.layer = 9;

		LineRenderer line_render = line_to_render.AddComponent< LineRenderer> ( ) as LineRenderer;

		line_render.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
		line_render.shadowCastingMode = 0;
		line_render.startWidth = line_width;
		line_render.endWidth = line_width;
		line_render.SetPosition (0, start);
		line_render.SetPosition( 1, end);

		return line_to_render;
	}

}
