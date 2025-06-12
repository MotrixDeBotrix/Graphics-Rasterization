using System.Diagnostics;
using INFOGR2025TemplateP2;
using INFOGR2025TemplateP2.Architecture;
using OpenTK.Mathematics;

namespace Template
{
    class MyApplication
    {
        // member variables
        public Surface screen;                  // background surface for printing etc.
        Mesh? teapot, floor;                    // meshes to draw using OpenGL
        Light? light;
        float a = 0;                            // teapot rotation angle
        readonly Stopwatch timer = new();       // timer for measuring frame duration
        Shader? shader;                         // shader to use for rendering
        Shader? postproc;                       // shader to use for post processing
        Texture? wood;                          // texture to use for rendering
        RenderTarget? target;                   // intermediate render target
        ScreenQuad? quad;                       // screen filling quad for post processing
        readonly bool useRenderTarget = true;   // required for post processing

        SceneGraphNode scene = new(Matrix4.Identity, null, null);

        // constructor
        public MyApplication(Surface screen)
        {
            this.screen = screen;
        }
        // initialize
        public void Init()
        {
            // load teapot
            teapot = new Mesh("../../../assets/teapot.obj");
            floor = new Mesh("../../../assets/floor.obj");

            light = new();

            // initialize stopwatch
            timer.Reset();
            timer.Start();
            // create shaders
            shader = new Shader("../../../shaders/vs.glsl", "../../../shaders/fs.glsl");
            postproc = new Shader("../../../shaders/vs_post.glsl", "../../../shaders/fs_post.glsl");
            // load a texture
            wood = new Texture("../../../assets/wood.jpg");
            // create the render target
            if (useRenderTarget) target = new RenderTarget(screen.width, screen.height);
            quad = new ScreenQuad();
        }

        // tick for background surface
        public void Tick()
        {
        }

        // tick for OpenGL rendering code
        public void RenderGL()
        {
            float frameDuration = timer.ElapsedMilliseconds;
            timer.Restart();

            float angle90degrees = MathF.PI / 2;
            Matrix4 teapotObjectToWorld = Matrix4.CreateScale(0.5f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            Matrix4 floorObjectToWorld = Matrix4.CreateScale(4.0f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 1, 0), a);
            Matrix4 lightObjectToWorld = Matrix4.CreateScale(4.0f) * Matrix4.CreateFromAxisAngle(new Vector3(0, 2, 0), a);
            Matrix4 worldToCamera = Matrix4.CreateTranslation(new Vector3(0, -14.5f, 0)) * Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 0), angle90degrees);
            Matrix4 cameraToScreen = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(60.0f), (float)screen.width / screen.height, 0.1f, 1000);
            Matrix4 viewProjection = worldToCamera * cameraToScreen;

            var teapotNode = new SceneGraphNode(teapotObjectToWorld, teapot, null);
            var floorNode = new SceneGraphNode(floorObjectToWorld, floor, null);
            var lightNode = new SceneGraphNode(lightObjectToWorld, null, light);
            scene = new SceneGraphNode(Matrix4.Identity, null, null);
            scene.AddChild(teapotNode);
            scene.AddChild(floorNode);
            scene.AddChild(lightNode);

            a += 0.001f * frameDuration;
            if (a > 2 * MathF.PI) a -= 2 * MathF.PI;

            if (useRenderTarget && target != null && quad != null)
            {
                target.Bind();
            }

            if (shader != null && wood != null)
            {
                scene.Render(Matrix4.Identity, viewProjection, shader, wood);
            }

            if (useRenderTarget && target != null && quad != null)
            {
                target.Unbind();

                if (postproc != null)
                {
                    quad.Render(postproc, target.GetTextureID());
                }
            }
        }

    }
}