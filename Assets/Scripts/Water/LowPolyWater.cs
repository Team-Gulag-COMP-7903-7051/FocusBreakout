using UnityEngine;

namespace LowPolyWater {
    public class LowPolyWater : MonoBehaviour {
        public float WaveHeight = 0.5f;
        public float WaveFrequency = 0.5f;
        public float WaveLength = 0.75f;

        //Position where the waves originate from
        public Vector3 WaveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);

        MeshFilter _meshFilter;
        Mesh _mesh;
        Vector3[] _vertices;

        private void Awake() {
            //Get the Mesh Filter of the gameobject
            _meshFilter = GetComponent<MeshFilter>();
        }

        void Start() {
            CreateMeshLowPoly(_meshFilter);
        }

        /// <summary>
        /// Rearranges the mesh vertices to create a 'low poly' effect
        /// </summary>
        /// <param name="mf">Mesh filter of gamobject</param>
        /// <returns></returns>
        MeshFilter CreateMeshLowPoly(MeshFilter mf) {
            _mesh = mf.sharedMesh;

            //Get the original vertices of the gameobject's mesh
            Vector3[] originalVertices = _mesh.vertices;

            //Get the list of triangle indices of the gameobject's mesh
            int[] triangles = _mesh.triangles;

            //Create a vector array for new vertices 
            Vector3[] vertices = new Vector3[triangles.Length];
            
            //Assign vertices to create triangles out of the mesh
            for (int i = 0; i < triangles.Length; i++) {
                vertices[i] = originalVertices[triangles[i]];
                triangles[i] = i;
            }

            //Update the gameobject's mesh with new vertices
            _mesh.vertices = vertices;
            _mesh.SetTriangles(triangles, 0);
            _mesh.RecalculateBounds();
            _mesh.RecalculateNormals();
            this._vertices = _mesh.vertices;

            return mf;
        }
        
        void Update() {
            GenerateWaves();
        }

        /// <summary>
        /// Based on the specified wave height and frequency, generate 
        /// wave motion originating from waveOriginPosition
        /// </summary>
        void GenerateWaves() {
            for (int i = 0; i < _vertices.Length; i++) {
                Vector3 v = _vertices[i];

                //Initially set the wave height to 0
                v.y = 0.0f;

                //Get the distance between wave origin position and the current vertex
                float distance = Vector3.Distance(v, WaveOriginPosition);
                distance = (distance % WaveLength) / WaveLength;

                //Oscilate the wave height via sine to create a wave effect
                v.y = WaveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * WaveFrequency
                + (Mathf.PI * 2.0f * distance));
                
                //Update the vertex
                _vertices[i] = v;
            }

            //Update the mesh properties
            _mesh.vertices = _vertices;
            _mesh.RecalculateNormals();
            _mesh.MarkDynamic();
            _meshFilter.mesh = _mesh;
        }
    }
}
