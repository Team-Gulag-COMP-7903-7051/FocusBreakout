using UnityEngine;
using UnityEngine.UI;

//Class for adding ability for "picking up objects"
//This class despawns the Object when it collides an object with the Player tag
//and changes the material on the Exit prefab and its children.

public class ObjectsToCollect : MonoBehaviour {

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _exit; //Exit prefab
    [SerializeField] private GameObject _plexusObj; //Plexus child object in Exit prefab
    [SerializeField] private Material _plexusWinMaterial; //Material to change on _plexusObj

    private GameObject _keyUI; //Key image on the UI
    

    private void Start() {
        _keyUI = GameObject.Find("UIKey");
    }

    private void OnTriggerEnter(Collider player) {
        if (player.gameObject.GetComponent("Player")) {
            Debug.Log("Object hit by Player");
            gameObject.SetActive(false);
        }

        if (gameObject.name == "Glitch(Key)") {
            _keyUI.GetComponent<Image>().enabled = true;
            _gameManager.KeyCollected = true;
        }

        Component[] renderers = _exit.GetComponentsInChildren(typeof(Renderer)); //Get all children Renderers
        foreach (Renderer childRenderer in renderers) {
            Color exitWinColor = new Color(0f, 0.25f, 1f);
            childRenderer.material.SetColor("_Color", exitWinColor); //For each child, apply the exitWinColor (blue)
        }

        _plexusObj.GetComponent<Plexus>().lineMaterial = _plexusWinMaterial; //Change the Plexus material to Win Material on key pickup
    }
}
