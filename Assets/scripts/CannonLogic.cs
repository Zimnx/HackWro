using UnityEngine;
using System.Collections;

public class CannonLogic : MonoBehaviour
{
    public static bool cannonSetuping;
    public static bool firePowerSetuping;
    public static float firePower;
    public static float  firePowerMin;
    public static float firePowerMax;
    bool firePowerRising;

    void Start()
    {

        cannonSetuping = true;
        firePowerSetuping = false;
        firePowerMin = 2000;
        firePowerMax = 4000;
        firePower = firePowerMin;
        firePowerRising = true;

    }

    // Update is called once per frame
    void Update()
    {
        CannonSetting();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BirdLogic.m_fire = true;
            firePower = 3000;
        }
        if (firePowerSetuping)
            FirePowerSetting();

    }
    void CannonSetting()
    {
        if (cannonSetuping && Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 dir = Vector3.zero;
                dir.z = -touch.deltaPosition.x * 5f;
                dir.x = -touch.deltaPosition.y * 5f;
                transform.Rotate(dir * Time.deltaTime);
                CameraLogic.debug = dir.x + ":" + dir.y + ":" + dir.z; ;
            }
            if (touch.deltaTime > 0.2f)
            {
                cannonSetuping = false;
                firePowerSetuping = true;
            }
        }
    }
    void FirePowerSetting()
    {
        
        if (firePowerRising)
        {
            firePower += 10;
            if (firePower >= firePowerMax)
            {
                firePower = firePowerMax;
                firePowerRising = false;
            }
        }
        else
        {
            firePower -= 10;
            if (firePower <= firePowerMin)
            {
                firePower = firePowerMin;
                firePowerRising = true;
            }
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                BirdLogic.m_fire = true;
                firePowerSetuping = false;
                BirdLogic.m_birdFlying = true;
            }
        }

        
    }


}

