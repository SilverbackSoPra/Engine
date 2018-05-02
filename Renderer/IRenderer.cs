
namespace Monogame_Engine.Engine.Renderer
{
    interface IRenderer
    {

        void Render(RenderTarget target = null, Camera camera = null, Scene scene = null);

    }
}
