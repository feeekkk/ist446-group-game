using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NetworkSelect : MonoBehaviour {

	public GameObject hostButton;
	public GameObject hostText;
	public GameObject joinButton;
	public GameObject joinText;
	private bool hostHover = false;
	private bool joinHover = false;

	// Use this for initialization
	void Start () {
		hostButton.AddComponent<Rigidbody> ();
		joinButton.AddComponent<Rigidbody> ();
		hostButton.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
		joinButton.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 20)) {
			if (hit.collider != null) {
				if (hit.collider.attachedRigidbody.gameObject == hostButton) {
					hostHover = true;
					joinHover = false;
				}
				else if (hit.collider.attachedRigidbody.gameObject == joinButton) {
					hostHover = false;
					joinHover = true;
				}
			}
		}
		else
		{
			hostHover = false;
			joinHover = false;
		}
		if (hostHover && Input.GetMouseButtonDown(0)) {
			SceneManager.LoadScene("HostMenu");
		} else {

		}
		if (joinHover && Input.GetMouseButtonDown(0)) {
			SceneManager.LoadScene("JoinMenu");
		} else {

		}
	}
}
