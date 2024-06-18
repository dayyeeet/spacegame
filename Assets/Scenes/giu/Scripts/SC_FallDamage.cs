using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FallDamage : MonoBehaviour
{
    public float calculateImpactForce (Rigidbody r, float vyCache, float time)
    {
        float acceleration = (r.velocity.y - vyCache) / time;
        float impactForce;
        return impactForce = r.mass * Mathf.Abs(acceleration);
    }

     public float calculateFallDamage(
         float impactForce, float hpImpactThreshold, float hpSmallBruises, float hpNormalInjuries, float hpSeriousInjuries, float hpFatalInjuries, float hpDeath)
    {
        if (impactForce < hpImpactThreshold)
            return 0; // No damage
        else if (impactForce < hpSmallBruises)
            return Mathf.Round((float)Mathf.Lerp(1, 10,
                (impactForce - hpImpactThreshold) / (hpSmallBruises - hpImpactThreshold)) * 10.0f) * 0.1f;
        else if (impactForce < hpNormalInjuries)
            return Mathf.Round((float)Mathf.Lerp(10, 20,
                (impactForce - hpSmallBruises) / (hpNormalInjuries - hpSmallBruises)) * 10.0f) * 0.1f;
        else if (impactForce < hpSeriousInjuries)
            return Mathf.Round((float)Mathf.Lerp(20, 50,
                (impactForce - hpNormalInjuries) / (hpSeriousInjuries - hpNormalInjuries)) * 10.0f) * 0.1f;
        else if (impactForce < hpFatalInjuries)
            return Mathf.Round((float)Mathf.Lerp(50, 80,
                (impactForce - hpSeriousInjuries) / (hpFatalInjuries - hpSeriousInjuries)) * 10.0f) * 0.1f;
        else if (impactForce < hpDeath)
            return Mathf.Round((float)Mathf.Lerp(80, 100,
                (impactForce - hpFatalInjuries) / (hpDeath - hpFatalInjuries)) * 10.0f) * 0.1f;
        else
            return 100; // Death
    }
}
