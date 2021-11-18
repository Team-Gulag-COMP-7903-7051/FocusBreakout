using UnityEngine;
using UnityEditor;
using System.IO;

namespace LowPolyWater {
    public class GeneratePlane : ScriptableWizard {
        public string ObjectName;           //Optional name that can given to created plane gameobject

        public int WidthSegments = 1;       //Number of pieces for dividing plane vertically
        public int HeightSegments = 1;      //Number of pieces for dividing plane horizontally
        public float PlaneWidth = 1.0f;     
        public float PlaneHeight = 1.0f;    

        public bool AddCollider = false;    //Add box collider?
        public Material Material;           //By default, it is assigned to 'LowPolyWaterMaterial' in the editor

        static Camera cam;
        static Camera lastUsedCam;

        //Generated plane meshes are saved and loaded from Plane Meshes folder (you can change it to whatever you want)
        public static string AssetSaveLocation = "Assets/Low Poly Water/Plane Meshes/";

        [MenuItem("GameObject/LowPoly Water/Generate Water Plane...")]
        static void CreateWizard() {
            cam = Camera.current;
            // Hack because camera.current doesn't return editor camera if scene view doesn't have focus
            if (!cam) {
                cam = lastUsedCam;
            } else {
                lastUsedCam = cam;
            }

            //Check if the asset save location folder exists
            //If the folder doesn't exists, create it
            if (!Directory.Exists(AssetSaveLocation)) {
                Directory.CreateDirectory(AssetSaveLocation);
            }

            //Open Wizard
            DisplayWizard("Generate Water Plane", typeof(GeneratePlane));
        }

        void OnWizardUpdate() {
            //Max segment number is 254, because a mesh can't have more 
            //than 65000 vertices (254^2 = 64516 max. number of vertices)
            WidthSegments = Mathf.Clamp(WidthSegments, 1, 254);
            HeightSegments = Mathf.Clamp(HeightSegments, 1, 254);
        }

        private void OnWizardCreate() {
            //Create an empty gamobject
            GameObject plane = new GameObject();

            //If user hasn't assigned a name, by default object name is 'Plane'
            if (string.IsNullOrEmpty(ObjectName)) {
                plane.name = "Plane";
            } else {
                plane.name = ObjectName;
            }

            //Create Mesh Filter and Mesh Renderer components
            MeshFilter meshFilter = plane.AddComponent(typeof(MeshFilter)) as MeshFilter;
            MeshRenderer meshRenderer = plane.AddComponent((typeof(MeshRenderer))) as MeshRenderer;
            meshRenderer.sharedMaterial = Material;

            //Generate a name for the mesh that will be created
            string planeMeshAssetName = plane.name + WidthSegments + "x" + HeightSegments
                                        + "W" + PlaneWidth + "H" + PlaneHeight + ".asset";

            //Load the mesh from the save location
            Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath(AssetSaveLocation + planeMeshAssetName, typeof(Mesh));

            //If there isn't a mesh located under assets, create the mesh
            if (m == null) {
                m = new Mesh();
                m.name = plane.name;

                int hCount2 = WidthSegments + 1;
                int vCount2 = HeightSegments + 1;
                int numTriangles = WidthSegments * HeightSegments * 6;
                int numVertices = hCount2 * vCount2;

                Vector3[] vertices = new Vector3[numVertices];
                Vector2[] uvs = new Vector2[numVertices];
                int[] triangles = new int[numTriangles];
                Vector4[] tangents = new Vector4[numVertices];
                Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
                Vector2 anchorOffset = Vector2.zero;

                int index = 0;
                float uvFactorX = 1.0f / WidthSegments;
                float uvFactorY = 1.0f / HeightSegments;
                float scaleX = PlaneWidth / WidthSegments;
                float scaleY = PlaneHeight / HeightSegments;

                //Generate the vertices
                for (float y = 0.0f; y < vCount2; y++) {
                    for (float x = 0.0f; x < hCount2; x++) {
                        vertices[index] = new Vector3(x * scaleX - PlaneWidth / 2f - anchorOffset.x, 0.0f, y * scaleY - PlaneHeight / 2f - anchorOffset.y);

                        tangents[index] = tangent;
                        uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                    }
                }

                //Reset the index and generate triangles
                index = 0;
                for (int y = 0; y < HeightSegments; y++) {
                    for (int x = 0; x < WidthSegments; x++) {
                        triangles[index] = (y * hCount2) + x;
                        triangles[index + 1] = ((y + 1) * hCount2) + x;
                        triangles[index + 2] = (y * hCount2) + x + 1;

                        triangles[index + 3] = ((y + 1) * hCount2) + x;
                        triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                        triangles[index + 5] = (y * hCount2) + x + 1;
                        index += 6;
                    }
                }

                //Update the mesh properties (vertices, UVs, triangles, normals etc.)
                m.vertices = vertices;
                m.uv = uvs;
                m.triangles = triangles;
                m.tangents = tangents;
                m.RecalculateNormals();

                //Save the newly created mesh under save location to reload later
                AssetDatabase.CreateAsset(m, AssetSaveLocation + planeMeshAssetName);
                AssetDatabase.SaveAssets();
            }

            //Update mesh
            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            //If add collider is set to true, add a box collider
            if (AddCollider) {
                plane.AddComponent(typeof(BoxCollider));
            }

            //Add LowPolyWater as component
            plane.AddComponent<LowPolyWater>();
            
            Selection.activeObject = plane;
        }
    }
}
