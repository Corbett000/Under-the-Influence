using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carDrunkness : MonoBehaviour
{
    public carcontroller controller;

    // max drunk factor = .7
    public float drunkfactor = .3f;
    int framenum;

    Queue<int> eventqueue = new Queue<int>();


    Rigidbody2D carRigidbody2D;


    // Start is called before the first frame update
    void Start()
    {
        controller = this.gameObject.GetComponent<carcontroller>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var sentry = Random.Range(0, 4);
        framenum += 1;
        if (framenum % 2 == 0)
        {
            //run drunk events
            var chance = Random.Range(0, 1);
            eventqueue.Enqueue(sentry);

            if (chance < drunkfactor)
            {
                if (sentry == 1)
                {
                    controller.velocityCap *= drunkfactor;
                }
                else if (sentry == 2)
                {
                    controller.turnFactor *= drunkfactor;
                }
                else if (sentry == 3)
                {
                    DrunkSteering();

                }
            }
        }
        else if (eventqueue.Count>0) {
             //delete preivous drunk events

             var old = eventqueue.Dequeue();
             if (old==1)
             {
                controller.velocityCap /= drunkfactor;
             }else if (old == 2)
             {
                 controller.turnFactor /= drunkfactor;
             }else if (old == 3)
             {
                    controller.steeringInput -= (float)(drunkfactor * Mathf.Sin(Time.time));
             }

         }


        void DrunkSteering()
        {
            controller.steeringInput += (float)(drunkfactor * Mathf.Sin(Time.time));
        }
    }
}