
namespace LevelEditor.Engine.Postprocessing
{
    /// <summary>
    /// Represents the post-processing applied to a scene.
    /// </summary>
    class PostProcessing
    {

        public readonly Fxaa mFxaa;
        public float mSaturation;

        /// <summary>
        /// Constructs a <see cref="PostProcessing"/>.
        /// </summary>
        public PostProcessing()
        {

            mSaturation = 1.0f;
            mFxaa = new Fxaa();

        }

    }
}
