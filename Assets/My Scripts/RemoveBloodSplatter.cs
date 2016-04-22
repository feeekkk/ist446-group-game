using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RemoveBloodSplatter : MonoBehaviour
{
	public GameObject bloodSplatter;
	public int time;

	void Start(){
		Destroy (bloodSplatter, time);
	}

}

