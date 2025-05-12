using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleColorByPosition : MonoBehaviour
{
    public Color[] leftColor;
    public Color[] middleColor;
    public Color[] rightColor;
    public float colorShiftRate = 50;
    ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    public bool testPos = false;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }


    void LateUpdate()
    {
        int numParticles = ps.GetParticles(particles);

        /*print("posx = " + particles[0].position.x);
        print("posx1 = " + Mathf.Round(particles[0].position.x));
        print("posx2 = " + Mathf.Round(particles[0].position.x * 25));
        print("posx3 = " + Mathf.Round(particles[0].position.x * 25)/25);
        if(testPos)
        {
            for (int i = 0; i < numParticles; i++)
            {
                particles[i].position = new Vector3(
                    Mathf.Round(particles[i].position.x * 25)/25,
                    Mathf.Round(particles[i].position.y * 25)/25,
                    particles[i].position.z
                );
            }
        }

        ps.SetParticles(particles, numParticles);*/
    }


    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var enterData);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {

            if( enterData.GetCollider(i, 0) == ps.trigger.GetCollider(0))
            {
                //print("yoyo");

                if(Random.Range(0,100)<colorShiftRate)
                {
                    ParticleSystem.Particle p = enter[i];
                    p.startColor = leftColor[0];
                    enter[i] = p;
                }
            }
            else
            {
                if(Random.Range(0,100)<colorShiftRate)
                {
                    ParticleSystem.Particle p = enter[i];
                    p.startColor = rightColor[0];
                    enter[i] = p;
                }
                //print("yaya");
            }

            /*if(Random.Range(0,100)<colorShiftRate)
            {
                ParticleSystem.Particle p = enter[i];
                p.startColor = leftColor[0];
                enter[i] = p;
            }*/
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = middleColor[0];
            exit[i] = p;
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}
