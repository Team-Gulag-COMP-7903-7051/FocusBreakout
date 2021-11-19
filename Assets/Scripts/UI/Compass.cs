using UnityEngine.UI;
using UnityEngine;

public class Compass : MonoBehaviour{
	public RawImage CompassImage;
	public Transform Player;
	public Text CompassDirectionText;

	public void Update(){
		//Get a handle on the Image's uvRect
		CompassImage.uvRect = new Rect(Player.localEulerAngles.y / 360, 0, 1, 1);

		// Get a copy of your forward vector
		Vector3 forward = Player.transform.forward;

		// Zero out the y component of your forward vector to only get the direction in the X,Z plane
		forward.y = 0;
	}
}
