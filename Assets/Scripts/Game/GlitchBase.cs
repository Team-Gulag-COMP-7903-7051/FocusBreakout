using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GlitchBase))]
public class GlitchBase : MonoBehaviour {
    [SerializeField] private int _stickAmount;

    private List<GameObject> _stickList = new List<GameObject>();
    private List<int> _stickProbabilityList = new List<int>();
    private int _overallProbability;
    void Start() {
        _overallProbability = 0;

        int probabilityCounter = 0;
        int childCount = transform.childCount;

        // Get all the Glitch Stick children
        for (int i = 0; i < childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.CompareTag("GlitchStick")) {
                int probability = child.GetComponent<GlitchStick>().Probability;

                _stickList.Add(child);
                _stickProbabilityList.Add(probability);
                _overallProbability += probability;
            }
        }

        // Instantiate Glitch Sticks
        for (int i = 0; i < _stickAmount; i++) {
            int num = Random.Range(0, _overallProbability) + 1;

            for (int j = 0; j < _stickProbabilityList.Count; j++) {
                probabilityCounter += _stickProbabilityList[j];

                if (num <= probabilityCounter) {
                    GameObject child = Instantiate(_stickList[j], Vector3.zero, Quaternion.identity);
                    child.transform.parent = transform;
                    break;
                }
            }
            
        }
    }

    private void OnValidate() {
        if (_stickAmount < 0) {
            _stickAmount = 0;
        }
    }
}
