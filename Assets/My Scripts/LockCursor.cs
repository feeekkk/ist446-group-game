using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour
{
	bool isCursorLocked;

	// Use this for initialization
	void Start ()
	{
		isCursorLocked = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		CheckForInput ();
		CheckIfCursorShouldBeLocked ();
	}

	void ToggleCursorState ()
	{
		isCursorLocked = !isCursorLocked;
	}

	void CheckForInput ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			ToggleCursorState ();
		}
	}

	void CheckIfCursorShouldBeLocked ()
	{
		if (isCursorLocked) {
			Cursor.lockState = CursorLockMode.Locked; // locks cursor to middle of screen
			Cursor.visible = false; // hides cursor
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
