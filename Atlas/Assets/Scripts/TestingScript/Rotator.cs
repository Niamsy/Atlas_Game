using UnityEngine;

namespace TestingScript
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private float _degreesPerSecondes;
	
		void Update ()
		{
			transform.Rotate(0f, _degreesPerSecondes * Time.deltaTime, 0f);	
		}
	}
}
