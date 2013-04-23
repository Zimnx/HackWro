using UnityEngine;
using System.Collections;

public class BirdLogic : MonoBehaviour {


	float m_force = 4500f;
	bool m_exploded = false;
	bool m_shouldCameraFollow = false;
    float m_camera_distance;
    public static bool m_fire = false;
    public static bool m_birdFlying = false;
    int m_explode_count = 2;
    int passed_time = 0;
    bool change_camera = true;
    bool dupa = false;

	// Use this for initialization
    void Start()
    {
        GameObject.Find("Main Camera").camera.enabled = true;
        GameObject.Find("FinishCamera").camera.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount == 2)
        {
            Application.LoadLevel(0);
        }
		//Vector3 dir;// = new Vector3();
		//if (Input.GetKeyDown(KeyCode.RightArrow))
		//    dir = Vector3.right;
		//else if (Input.GetKeyDown(KeyCode.LeftArrow))
		//    dir = Vector3.left;
		//else if (Input.GetKeyDown(KeyCode.UpArrow))
		//    dir = Vector3.forward;
		//else if (Input.GetKeyDown(KeyCode.DownArrow))
		//    dir = -1 * Vector3.right;
		//else return;
		//    //dir.x = Input.acceleration.x * 10f;
		//    //dir.y = Input.acceleration.y * 10f;
		//    transform.Translate(dir);
		//    //transform.Translate(dir * Time.deltaTime);	

		if (m_fire)
		{
            m_fire = false;
			var rb = gameObject.AddComponent<Rigidbody>();
			var armata = GameObject.Find("cannon");
			if (armata != null)
			{
				rb.AddForce(armata.transform.up * CannonLogic.firePower);
                rb.mass = 20;
				m_shouldCameraFollow = true;
                m_camera_distance = 15f;
                Camera.main.fieldOfView = 92;
			}
		}
        passed_time++;

		updateCamera();
        if(m_birdFlying)
            changeDirection();
	}
    void changeDirection()
    {
        if (Input.accelerationEventCount > 0)
        {
            Vector3 dir = Vector3.zero;
            dir.x = Input.acceleration.x * 25f;
            //dir.y = -Input.acceleration.y * 25f;
            //transform.Rotate(dir * Time.deltaTime);
            //transform.Translate(dir * Time.deltaTime);
            Rigidbody rb = (Rigidbody)this.GetComponent<Rigidbody>();
            rb.AddForce(dir * rb.velocity.magnitude);
            transform.Rotate(dir.x / 12, 0, 0);
            //CameraLogic.debug = dir.x + ":" + dir.y + ":" + dir.z; ;
        }
    }

	void updateCamera()
	{
        if (!m_shouldCameraFollow)
            if (m_exploded && change_camera)
            { changeCamera(); return; }
            else
                return;
		//Camera.main.transform.position = this.transform.position;
        var roof = GameObject.Find("CameraHandleTarget");
        Vector3 target;
        if (roof == null) return; else target = roof.transform.position;
		Vector3 thisPos = transform.position;
		float mZ = (thisPos.z > target.z) ? 1 : -1;

       
        var dest = GameObject.Find("CameraHandle").transform.position;
        

        Camera.main.transform.position = dest;

		Camera.main.transform.LookAt(target);

        if (m_camera_distance >= 7f)
            m_camera_distance -= 0.1f;
	}

	void OnCollisionEnter(Collision collision)
	{
        if (!m_exploded && collision.collider.name != "cannon")
        {
            var s = this.GetComponent<Detonator>();
            if (s != null)
                s.Explode();
            m_exploded = true;
        }
        if (collision.collider.name != "cannon" && change_camera)
        {
            changeCamera();
            detonate();
        }
	}

    void detonate()
    {
        if (m_explode_count <= 0)
            return;
        m_explode_count--;

        Detonator expl = (Detonator)gameObject.AddComponent("Detonator");
        expl.size = 45;
        expl.duration = 5;
        expl.enabled = true;
        expl.explodeOnStart = true;
        expl.destroyTime = 550;
    }

    void changeCamera()
    {
        dupa = true;
        passed_time = 0;
        change_camera = false;
        Debug.Log("camera changed");
        GameObject.Find("Main Camera").camera.enabled = true;
        GameObject.Find("FinishCamera").camera.enabled = false;
        GameObject.Find("Main Camera").camera.tag = "MainCamera";
        GameObject.Find("FinishCamera").camera.tag = "Finish";
        m_shouldCameraFollow = false;
        
    }

	void startParticles()
	{
		var particleSystem = this.GetComponent<ParticleSystem>();
		particleSystem.Play();
	}

}
