using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PilotoStudio
{
    [ExecuteAlways]
    public class ParticleHandler : MonoBehaviour
    {
        public GameObject castParticle;
        public float castFXDuration;
        public GameObject loopingParticle;
        public float loopDuration;
        public GameObject endParticle;

        private ParticleSystem castParticleSystem;
        private ParticleSystem loopingParticleSystem;
        private ParticleSystem endParticleSystem;

        float startEmission;
        private void OnEnable()
        {
            castParticleSystem = castParticle.GetComponent<ParticleSystem>();
            loopingParticleSystem = loopingParticle.GetComponent<ParticleSystem>();
            endParticleSystem = endParticle.GetComponent<ParticleSystem>();
            if (!castParticleSystem || !loopingParticleSystem || !endParticleSystem)
            {
                Debug.LogError("ParticleHandler: Missing particle systems. Ensure they are referenced correctly.");
                return;
            }

            Cast();
        }

        public void Cast()
        {
            StartCoroutine(Flow());
        }

        IEnumerator Flow()
        {
            PlayParticles(castParticleSystem, castFXDuration);
            yield return new WaitForSeconds(castFXDuration);

            PlayParticles(loopingParticleSystem, loopDuration);
            yield return new WaitForSeconds(loopDuration);

            PlayParticles(endParticleSystem);
            yield return WaitUntilParticleSystemStops(endParticleSystem);
        }
        private IEnumerator WaitUntilParticleSystemStops(ParticleSystem particleSystem)
        {
            while (particleSystem.IsAlive(true))
            {
                yield return null;
            }
        }
        private void PlayParticles(ParticleSystem particleSystem, float duration = 0f)
        {
            particleSystem.gameObject.SetActive(true);
            var particleSystemMain = particleSystem.emission;

            if (startEmission == 0)
                startEmission = particleSystemMain.rateOverTimeMultiplier;

            if (particleSystem.main.startLifetime.constantMax == Mathf.Infinity)
                StartCoroutine(WaitUntilParticleSystemStops(particleSystem));
            else
                particleSystemMain.rateOverTimeMultiplier = startEmission;

            particleSystem.Play();

            if (duration > 0f && particleSystem.main.startLifetime.constantMax != Mathf.Infinity)
            {
                StartCoroutine(StopParticleAfterTime(particleSystem, duration));
            }
        }

        IEnumerator StopParticleAfterTime(ParticleSystem particleSystem, float duration)
        {
            yield return new WaitForSeconds(duration);
            var particleSystemMain = particleSystem.emission;
            particleSystemMain.rateOverTimeMultiplier = 0;
            //   particleSystem.gameObject.SetActive(false);
        }
    }




}