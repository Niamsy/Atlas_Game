using UnityEngine;

namespace TestingScript
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] private float _degreesPerSecondes = 45f;
	
		void Update ()
		{
			transform.Rotate(0f, _degreesPerSecondes * Time.deltaTime, 0f);	
		}
	}
}
