using UnityEngine;
using System.Collections;

public class BrickBlock : MonoBehaviour {

    const int maxHp = 150;
	int hp = maxHp;

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "block") {
			hp -= 20;
			//Debug.Log("Current hp : " + hp);
		}

        checkHp();
	}

    void checkHp()
    {
        if (hp <= 0)
        {
            //Detonator expl = (Detonator) gameObject.AddComponent("Detonator");
            //expl.size = 10;
            //expl.duration = 5;
            //expl.enabled = true;
            //expl.explodeOnStart = true;
            //expl.destroyTime = 150;
            //expl.autoCreateFireball = true;
            //expl.autoCreateGlow = false;
            //expl.autoCreateHeatwave = false;
            //expl.autoCreateSparks = false;

            //expl.autoCreateShockwave = false;
            //expl.autoCreateForce = false;

            //expl.fireballAMaterial = null;
            //expl.fireballBMaterial = null;
            //expl.shockwaveMaterial = null;
            //expl.heatwaveMaterial = null;
            //expl.glowMaterial = null;
            //expl.sparksMaterial = null;
            //expl.color = Color.gray;
            Destroy(this);
        }

        //if (hp <= (1 / 4) * maxHp)
        //    this.renderer.material.mainTexture = (Resources.Load("stone_diff_3") as Texture);
        //else if (hp <= (1 / 2) * maxHp)
        //    ;//this.renderer.material.mainTexture = (Resources.Load("stone_diff_2") as Texture);
        //else if (hp <= (3 / 4) * maxHp)
        //    this.renderer.material.mainTexture = (Resources.Load("stone_diff_1") as Texture);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
