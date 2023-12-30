using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Create_shapes : MonoBehaviour
{

    [SerializeField] GameObject base_cube;
    [SerializeField] GameObject Game_over;
    // Start is called before the first frame update
    public bool hit_down = false;
    public bool hit_next = false;
    float steps = 0.5f;
    float next_action = 0.0f;

    


    List<GameObject> current_shape=new List<GameObject>();

    List<List<GameObject>> all_cubes = new List<List<GameObject>>();
    List<List<GameObject>> all_shapes = new List<List<GameObject>>();
    public List<bool> all_shapes_moving = new List<bool>();
    void Start()
    {
        Create_Shape();


        for (int i = 0; i < 8; i++)
        {
            all_cubes.Add(new List<GameObject>());

        }
    }


    void set_color(GameObject go)
    { go.GetComponent<Renderer>().material.color = new Color(Random.Range(0.0f, 1.0f) , Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f) , 1);
        Debug.Log(go.GetComponent<Renderer>().material.color);
    }

    void Create_Shape()
    {


        GameObject go = Instantiate(base_cube);

        go.GetComponent<cube_baser>().manager = gameObject;
        go.transform.position= new Vector3 (0, 5, 0);
        current_shape.Add(go);
        set_color( go);
        all_shapes.Add(new List<GameObject>());


 


        all_shapes[all_shapes.Count-1].Add(go);
        go.GetComponent<cube_baser>().ma_shape = all_shapes.Count - 1;
        int numb_cube = Random.Range(2, 5); ;
      //  Debug.Log(side_up);
      //  Debug.Log(left_right);


        for (int i = 0; i < numb_cube; ++i)
        {
            Vector3 where= new Vector3( 0,0,0);
            bool val = true;
            while (val) {
                val = false; 
                int side_up = Mathf.RoundToInt(Random.Range(-1, 1)) * 2 + 1; ; ;
                int left_right = Mathf.RoundToInt(Random.Range(-1, 1)) * 2 + 1; ;


                if (side_up > 0)
                {
                    where = new Vector3(left_right, 0, 0);
                } else
                {
                    where = new Vector3(0, left_right, 0);

                }
                for (int j = 0; j < current_shape.Count; ++j)
                {
                    if (current_shape[j].transform.position == where + go.transform.position)
                    {
                        val = true;
                    }
                }
            }
             go = Instantiate(go, go.transform.position+ where, new Quaternion() );
            go.GetComponent<cube_baser>().manager = gameObject;
            current_shape.Add(go);
            all_shapes[all_shapes.Count-1].Add(go);
            go.GetComponent<cube_baser>().ma_shape = all_shapes.Count-1;

            all_shapes_moving.Add(true);
        }
       
    }
    public void gameover()
    {

        Game_over.SetActive(true);
        
    }
    bool left_ok()
    {
        for ( int i = 0; i< current_shape.Count; i++)
        {
            for (int j = 0; j < all_cubes.Count; j++)
            {
                for (int k = 0; k < all_cubes[j].Count; k++)
                {
                    if ((current_shape[i].transform.position + new Vector3(-1, 0, 0)) == all_cubes[j][k].transform.position)
                    {
                        return false;
                    }
                }
            }
            if (current_shape[i].transform.position.x < -3)
            {

                Debug.Log("leftnoootok");
                Debug.Log(current_shape[i].transform.position.x);
                return false;
            }

        }   

        return true;

    }

    void move_left()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {


            current_shape[i].transform.position+= new  Vector3(-1,0,0 )  ;
           

        }

    }


    void add_current_to_all()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {

            if((int)current_shape[i].transform.position.y + 3> 7.5f)
            {
                gameover();
                return;
            }
            all_cubes[(int)current_shape[i].transform.position.y+3].Add(current_shape[i]);


        }

    }
    int first_line()
    {
        for (int i = 0; i < all_cubes.Count; i++)
        {
            if (all_cubes[i].Count > 8)
            {
                return i;

            }

        }
        return -1;
    }
    void check_for_line()
    {
        int a_line = first_line();
        if (a_line == -1) return;

        for (int j = 0; j < all_cubes[a_line].Count; j++)
        {
            all_cubes[a_line][j].transform.position += new Vector3(-20, 0, 0);

        }
        for (int i = 0; i < all_cubes[a_line].Count; i++)
        {
            all_shapes[all_cubes[a_line][i].GetComponent<cube_baser>().ma_shape].Remove(all_cubes[a_line][i]);
        }
        all_cubes[a_line].Clear();
    
        
        for (int i = 0;i < all_cubes.Count;i++)
        {
            for (int j = 0; j < all_cubes[i].Count; j++)
            {
                if (all_cubes[i][j].transform.position.y+3 > a_line)
                {
                    all_cubes[i][j].transform.position += new Vector3(0, -1, 0);
                   
                }
            }

        }



        reorganize_all_cubes();
    }


    void reorganize_all_cubes()
    {
     
        List<List<GameObject>> Alist = new List<List<GameObject>>();
  
        for (int i = 0; i < 8; i++)
        {
            Alist.Add(new List<GameObject>());

        }

        for (int i = 0; i < all_cubes.Count; i++)
        {
            for (int j = 0; j < all_cubes[i].Count; j++)
            {

                Alist[(int)all_cubes[i][j].transform.position.y + 3].Add(all_cubes[i][j]);
            }
        }
        all_cubes.Clear();
        all_cubes = Alist;
    }
    bool rigth_ok()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {


            for (int j = 0; j < all_cubes.Count; j++)
            {
                for (int k = 0; k < all_cubes[j].Count; k++)
                {
                    if ((current_shape[i].transform.position + new Vector3(1, 0, 0)) == all_cubes[j][k].transform.position)
                    {

                        return false;
                    }
                }
            }
            if (current_shape[i].transform.position.x > 3)
            {
                return false;
            }

        }

        return true;

    }

    void move_rigth()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {
            current_shape[i].transform.position += new Vector3(1, 0, 0);


        }



    }

    void move_down()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {
            current_shape[i].transform.position += new Vector3(0, -1, 0);


        }



    }
    void move_up()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {
            current_shape[i].transform.position += new Vector3(0, 1, 0);


        }

    }
    void set_all_floor ()
    {
        for (int i = 0; i < current_shape.Count; i++)
        {
            current_shape[i].tag = "floor";


        }



    }

    void move_shape_down(int i)
    {
        for (int j = 0; j < all_shapes[i].Count; j++)
        {
            all_shapes[i][j].transform.position += new Vector3(0,-1,0);


        }
    }
   public void move_shape_up(int i)
    {
        for (int j = 0; j < all_shapes[i].Count; j++)
        {
            all_shapes[i][j].transform.position += new Vector3(0, 1, 0);


        }
    }
    public void set_shape_floor(int i)
    {
        if (all_shapes[i][0].tag == "floor")
            return;
        for (int j = 0; j < all_shapes[i].Count; j++)
        {

            all_shapes[i][j].tag = "floor";
        }
       move_shape_up(i);
    }

    public void set_shape_not_floor(int i)
    {
        for (int j = 0; j < all_shapes[i].Count; j++)
        {
            all_shapes[i][j].tag = "edge";
        }
    }



    // Update is called once per frame
    void Update()
    {




        if (Game_over.active)
            return;





        if (Input.GetKeyDown("a"))
        {
            Debug.Log("a");
            if (left_ok())
            {
                Debug.Log("leftok");
                move_left();
            }

        }
        if (Input.GetKeyDown("d"))
        {
            if (rigth_ok())
            {
                move_rigth();
            }
        }



        if (Time.time > next_action)
        {
            next_action += steps;
            for (int i = 0; i < all_shapes.Count; i++)
            {
                if (all_shapes_moving[i])
                {
                    move_shape_down(i);
                }

            }
            bool all_fixed = true;
            for (int i = 0; i < all_shapes.Count; i++)
            {


                all_fixed = all_fixed && !all_shapes_moving[i];


            }
            if (all_fixed)
            {



                add_current_to_all();
                current_shape.Clear();

                check_for_line();
                
                
                
                Create_Shape();
            }
        }



    }
}
