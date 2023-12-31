using UnityEngine;

namespace BattleCity
{

	public class CheckMove : MonoBehaviour
	{

		public GameObject target;

		void OnTriggerStay2D(Collider2D coll)
		{
			if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player") target = coll.gameObject;
		}

		void OnTriggerExit2D(Collider2D coll)
		{
			if (coll.gameObject.tag == "Enemy" || coll.gameObject.tag == "Player") target = null;
		}
	}
}