using UnityEngine;
using System.Collections;

public class RecursionExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int factorial = Factorial (25);
		Debug.Log ("Factorial = " + factorial);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static int Factorial(int n) {
		if (n == 1) {
			return 1;
		} else {
			return n * Factorial (n - 1);
		}
	}
}
