using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/* Author : Nate Cortes
 * Manages Formulas from the top UI element. 
 * This class watches for changes in the UI element's data. 
 * All game objects that need the player's input will get the linear algebra representation from this class.
 * Supports operations such as formating, logging, and data passing
 * Made for CPI441 capstone. 
 */

public class formula_controller : MonoBehaviour
{
    ////////////////////// 
    //( constant_1 * vector_1) + ( constant_2 * vector_2) = output
    public int constant_1;
    public int constant_2;
    public Vector2 vector_1;
    public Vector2 vector_2;
    public Vector2 output;

    //////////////////////
    //References to UI elements
    public GameObject player;
    public GameObject puzzleManager;
    public LineRenderer line_1, line_2;
    public Transform c1, c2, v1, v2, destination;
    public bool change = false;

    //////////////////////
    //Log output
    public Text log;

    //////////////////////
    //for determining changes in the UI
    int con_1; //values of constant ui buttons
    int con_2;
    int v1_cc; //positive value if dropper element has choice attached to it
    int v2_cc;
    Transform v1_prev_child = null, v2_prev_child = null;

    //////////////////////

    void Awake()
    {
        GameObject formula_panel = GameObject.FindGameObjectWithTag("Formula");
        constant_1 = 0; constant_2 = 0;
        vector_1 = Vector2.zero; vector_2 = Vector2.zero; output = Vector2.zero;
        player = GameObject.Find("Player");

        line_1 = GameObject.FindGameObjectWithTag("PathToRender").GetComponent<LineRenderer>();
        line_1.positionCount = 3;
        line_2 = GameObject.FindGameObjectWithTag("PathPreviouslyRendered").GetComponent<LineRenderer>();
        line_2.positionCount = 1;
        line_2.SetPosition(0, new Vector3(player.transform.position.x, 0f, player.transform.position.z));

        c1 = formula_panel.transform.GetChild(0);
        constant_1 = c1.GetComponent<constant_counter>().constant;
        v1 = formula_panel.transform.GetChild(2);

        c2 = formula_panel.transform.GetChild(4);
        constant_2 = c2.GetComponent<constant_counter>().constant;
        v2 = formula_panel.transform.GetChild(5);

        destination = formula_panel.transform.GetChild(7);

    }

    void Update()
    {
        con_1 = c1.GetComponent<constant_counter>().constant; //values of constant ui buttons
        con_2 = c2.GetComponent<constant_counter>().constant;
        v1_cc = v1.childCount; //positive value if dropper element has choice attached to it
        v2_cc = v2.childCount;

        //Check for Drag & Drop Changes
        if (constant_1 != con_1)
        {
            constant_1 = con_1;
            change = true;
        }
        if (constant_2 != con_2)
        {
            constant_2 = con_2;
            change = true;
        }

        if (v1_cc > 0)
        {
            Vector2 vect_1 = v1.GetChild(0).GetComponent<choice_holder>().choice;
            if (vector_1 != vect_1)
            {
                vector_1 = vect_1;
                change = true;
            }

            if (v1.transform.GetChild(0) != v1_prev_child)
                c1.GetComponent<constant_counter>().reset();

            v1_prev_child = v1.transform.GetChild(0);
        }
        else
        {
            vector_1 = Vector2.zero;
            v1_prev_child = null;
        }
        if (v2_cc > 0)
        {
            Vector2 vect_2 = v2.GetChild(0).GetComponent<choice_holder>().choice;
            if (vector_2 != vect_2)
            {
                vector_2 = vect_2;
                change = true;
            }

            if (v2.transform.GetChild(0) != v2_prev_child)
                c2.GetComponent<constant_counter>().reset();

            v2_prev_child = v2.transform.GetChild(0);
        }
        else
        {
            vector_2 = Vector2.zero;
            v2_prev_child = null;
        }




        if (vector_1 == Vector2.zero &&
            vector_2 == Vector2.zero)
        {
            line_1.gameObject.SetActive(false);
        }
        else
        {
            line_1.gameObject.SetActive(true);
        }

        if (change)
        {

            //construct output
            Vector2 outp = (constant_1 * vector_1) + (constant_2 * vector_2);
            if (outp != output)
            {
                output = outp;
                destination.GetChild(0).GetComponent<Text>().text = output.x.ToString("F0") + "\n" + output.y.ToString("F0");
            }

            //construct "Future Sight" 
            Vector3[] points = new Vector3[3];
            Vector3 start = player.transform.position;
            points[0] = start - new Vector3(0, 2.5f, 0);
            points[1] = constant_1 * new Vector3(vector_1.x, 0.0f, vector_1.y) + points[0];
            points[2] = constant_2 * new Vector3(vector_2.x, 0.0f, vector_2.y) + points[1];

            line_1.SetPositions(points);

        }
        change = false;

    }

    public void log_formula() { log.text += print_formula() + "\n"; }

    public string print_formula()
    {//formats a vector equation for logging, "( c1 * < x1, y1>) + ( c2 * < x2, y2>) = <dx, dy>"
        return "( " + constant_1 + " * " + string_vector(vector_1) + ") + ( " + constant_2 + " * " + string_vector(vector_2) + ") = " + string_vector(output);
    }

    public string string_vector(Vector2 vect)
    {//formats a vector, < x, y>
        return "< " + vect.x.ToString() + ", " + vect.y.ToString() + ">";
    }

    public void move_player()
    {
        /************************************************/
        /* STEP 1: Send movement Coordinates to Player  */
        /************************************************/
        log_formula();
        if (!player.GetComponent<PlayerMovement>().is_moving() &&
            ((constant_1 != 0 && vector_1 != Vector2.zero) ||
                (constant_2 != 0 && vector_2 != Vector2.zero))
        )
        {
            Vector3[] move = new Vector3[2];
            move[0] = constant_1 * new Vector3(vector_1.x, 0f, vector_1.y);
            move[1] = constant_2 * new Vector3(vector_2.x, 0f, vector_2.y);

            player.GetComponent<PlayerMovement>().Move(move);

            path_trace();

            reset();
        }
        else if(!((constant_1 != 0 && vector_1 != Vector2.zero) ||
                (constant_2 != 0 && vector_2 != Vector2.zero)))
        {
            puzzleManager.GetComponent<UFO_PuzzleManager>().TestSuccess(player.transform.position);
        }
    }

    public void path_trace()
    {
        /************************************************/
        /* STEP 2: Send Coordinates to Path Renderer    */
        /************************************************/
        Vector3[] points = new Vector3[line_1.positionCount];
        line_1.GetPositions(points);
        points[1].y = 0.0f;
        points[2].y = 0.0f;
        if (constant_1 != 0)
        {
            line_2.positionCount = line_2.positionCount + 1;
            line_2.SetPosition(line_2.positionCount - 1, points[1]);
        }

        if (constant_2 != 0)
        {
            line_2.positionCount = line_2.positionCount + 1;
            line_2.SetPosition(line_2.positionCount - 1, points[2]);
        }

        //line_2.Simplify(1.0f);
    }

    public void reset()
    {
        /************************************************/
        /* STEP 3: Clear UI Elements holding formula    */
        /************************************************/
        c1.GetComponent<constant_counter>().reset();
        c2.GetComponent<constant_counter>().reset();
        /*if( v1)
			v1.GetComponent< choice_holder> ().update_choice (new Vector3 (0, 0, 0));
		if( v2)
			v2.GetComponent< choice_holder> ().update_choice (new Vector3 (0, 0, 0));*/
    }
}
