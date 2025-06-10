using OpenTK.Mathematics;
using Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFOGR2025TemplateP2.Architecture
{
    internal class SceneGraphNode
    {
        Matrix4 objectToParent;
        Mesh mesh;
        List<SceneGraphNode> children;

        public SceneGraphNode(Matrix4 oTP, Mesh m)
        {
            objectToParent = oTP;
            mesh = m;

            children = new();
        }

        public void AddChild(SceneGraphNode toAdd)
        {
            children.Add(toAdd);
        }

        void Render(Matrix4 cameraMatrix)
        {
            //Render self
            Matrix4 result = cameraMatrix * objectToParent * mesh.localTransform; //Zoiets

            //Render children (if they exist)
            if (children.Count > 0)
            {
                foreach (SceneGraphNode child in children)
                {
                    child.Render(cameraMatrix);
                }
            }
        }
    }
}
