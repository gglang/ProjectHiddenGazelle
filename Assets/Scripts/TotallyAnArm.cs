using UnityEngine;
using System.Collections;

public class TotallyAnArm : MonoBehaviour {
    /*
    bool canFire = true;

    public float swingDuration;
    public float totalDuration;

    public float maxRotation;

    float swingTimer;

    enum SwingState
    {
        Swinging,
        Static,
        Recovering,
        Idle
    }

    SwingState state;

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(swingDuration);
        canFire = false;
        EnableChild(true);
        state = SwingState.Swinging;
        yield return new WaitForSeconds(swingDuration);
        state = SwingState.Static;
        yield return new WaitForSeconds(activeDuration);
        state = SwingState.Recovering;
        yield return new WaitForSeconds(recoveryDuration);
        state = SwingState.Idle;
        EnableChild(false);
        canFire = true;
    }

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        swingTimer += Time.deltaTime;
        if (swingTimer < swingDuration)
        {
            state = SwingState.Swinging;
        }
        if (swingTimer > swingDuration)
        {
            state = SwingState.Static;
        }
        if (swingTimer > swingDuration + activeDuration)
        {
            state = SwingState.Recovering;
        }
        if (swingTimer > swingDuration + activeDuration + recoveryDuration)
        {
            state = SwingState.Idle;
            EnableChild(false);
        }
        //Debug.DrawRay(transform.position, transform.position+transform.forward*10, Color.blue, 5f, false);
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (state == SwingState.Idle)
            {
                swingTimer = 0f;
                EnableChild(true);
            }
        }
        if (state == SwingState.Swinging)
        {
            if (transform.eulerAngles.x < maxRotation)
            {
                Vector3 eulerAngles = transform.eulerAngles;
                eulerAngles.x += maxRotation * Time.deltaTime / swingDuration;
            }
        }
        else if (state == SwingState.Recovering)
        {
            if (transform.eulerAngles.x > 0)
            {
                Vector3 eulerAngles = transform.eulerAngles;
                eulerAngles.x -= maxRotation * Time.deltaTime / recoveryDuration;
            }
        }
    }

    void EnableChild(bool enabled)
    {
        GetComponentInChildren<Renderer>().enabled = enabled;
        GetComponentInChildren<Collider>().enabled = enabled;
    }*/
}
