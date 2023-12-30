using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_baser : MonoBehaviour
{

   public bool has_left;
    public bool has_right;
    public bool has_down;
    public bool has_up;
    public GameObject manager;
    bool fixed_now = false;
    public int ma_shape;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "floor")
        {
            manager.GetComponent<Create_shapes>().all_shapes_moving[ma_shape] = false;
            
            manager.GetComponent<Create_shapes>().set_shape_floor(ma_shape);

            if (transform.position.y > 4.5f)
            {
                manager.GetComponent<Create_shapes>().gameover();
            }

        }

    }
    // Update is called once per frame
    void Update()
    {

        
  

    }
}
