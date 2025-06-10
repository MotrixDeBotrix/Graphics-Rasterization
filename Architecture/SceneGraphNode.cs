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

        void Render()
        {

        }
    }
}
