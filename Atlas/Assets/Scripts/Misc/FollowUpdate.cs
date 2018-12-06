using UnityEngine;

namespace Misc
{
	public class FollowUpdate : MonoBehaviour
	{
		public Transform Target;
		
		private void Update ()
		{
			if (Target != null)
			{
				transform.position = Target.position;
				transform.rotation = Target.rotation;
				transform.localScale = Target.localScale;
			}
		}
	}
}
