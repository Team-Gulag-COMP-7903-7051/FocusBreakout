using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(GlitchBaseTest))]
public class GlitchBaseTest : MonoBehaviour {
    [SerializeField] private int _stickAmount;
    [SerializeField] private GameObject[] _stickArray;

    private int[] _stickProbabilityArray;
    void Start() {
        if (_stickArray.Length == 0) {
            throw new ArgumentException("StickArray in GlitchBase cannot be empty.");
        }
        _stickProbabilityArray = new int[_stickArray.Length];
        int overallProbability = 0;

        // Get overall probability from Glitch Sticks
        for (int i = 0; i < _stickArray.Length; i++) {
            GameObject obj = _stickArray[i];
            if (obj.CompareTag("GlitchStick")) {
                int probability = obj.GetComponent<GlitchStick>().Probability;

                _stickProbabilityArray[i] = probability;
                overallProbability += probability;
            }
        }

        // Instantiate Glitch Sticks
        for (int i = 0; i < _stickAmount; i++) {
            int num = Random.Range(0, overallProbability) + 1;
            int probabilityCounter = 0;

            for (int j = 0; j < _stickProbabilityArray.Length; j++) {
                probabilityCounter += _stickProbabilityArray[j];

                if (num <= probabilityCounter) {
                    GameObject obj = Instantiate(_stickArray[j], Vector3.zero, Quaternion.identity);
                    obj.transform.parent = transform;
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
