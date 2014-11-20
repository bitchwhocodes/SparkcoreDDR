using UnityEngine;
using System.Collections;
using System.Net;
using System.IO;

public class Plane : MonoBehaviour
{
		public Vector2 jumpForce = new Vector2 (0, 300);
		public WWW www;
	 
		// Use this for initialization
		void Start ()
		{
				InvokeRepeating ("GetSparkData", 0.2f, 0.2f);
		}

		void GetSparkData ()
		{

				www = new WWW ("https://api.spark.io/v1/devices/{YOUR_SPARK_CORE}/pressure?access_token={YOUR_ACCESSTOKEN}");
				// Wait for download to complete
				StartCoroutine (WaitForRequest (www));



		}

		IEnumerator WaitForRequest (WWW www)
		{
				yield return www;
			
				// check for errors
				if (www.error == null) {
						Debug.Log ("WWW Ok!: " + www.data);
				} else {
						Debug.Log ("WWW Error: " + www.error);
				}   

				JSONObject jo = new JSONObject (www.text);
				string result = jo ["result"].ToString ();
				int value = int.Parse (result);
				if (value < 20) {
						doJump ();
				}
		}

		void doJump ()
		{

				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.AddForce (jumpForce);
		}
	
		// Update is called once per frame
		void Update ()
		{
				// Ok, we can do this for the spacebar but ... we will use the other 
				if (Input.GetKeyUp ("space")) {
						//Debug.Log ("we captured the space bar");
						doJump ();
				}


		}

		void OnCollisionEnter2D (Collision2D other)
		{
				Debug.Log ("collision enter");
				Die ();
		}

		void Die ()
		{

				// we are restarting this game by reloading it
				Application.LoadLevel (Application.loadedLevel);
		}
}
