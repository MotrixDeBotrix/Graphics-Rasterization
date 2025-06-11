using OpenTK.Mathematics;
using Template;
using System.Collections.Generic;

namespace INFOGR2025TemplateP2.Architecture
{
    internal class SceneGraphNode
    {
        private Matrix4 localTransform;
        private Mesh? mesh;
        private List<SceneGraphNode> children;

        public SceneGraphNode(Matrix4 localTransform, Mesh? mesh)
        {
            this.localTransform = localTransform;
            this.mesh = mesh;
            children = new List<SceneGraphNode>();
        }

        public void AddChild(SceneGraphNode child)
        {
            children.Add(child);
        }

        public void Render(Matrix4 parentToWorld, Matrix4 viewProjection, Shader shader, Texture texture)
        {
            Matrix4 objectToWorld = localTransform * parentToWorld;
            Matrix4 objectToScreen = objectToWorld * viewProjection;

            if (mesh != null)
            {
                mesh.Render(shader, objectToScreen, objectToWorld, texture);
            }

            foreach (SceneGraphNode child in children)
            {
                child.Render(objectToWorld, viewProjection, shader, texture);
            }
        }
    }
}
