using UnityEngine;

namespace Assets.Enviroment.SineVFX.LivingParticles.Resources.Scripts
{
	public class LivingParticleController : MonoBehaviour {

		public Transform affector;

		private ParticleSystemRenderer psr;

		void Start () {
			psr = GetComponent<ParticleSystemRenderer>();
		}
	
		void Update () {
			psr.material.SetVector("_Affector", affector.position);
		}
	}
}
