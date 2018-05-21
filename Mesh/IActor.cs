
namespace LevelEditor.Engine.Mesh
{

    /// <summary>
    /// Used to add any class in a scene instance.
    /// </summary>
    interface IActor
    {

        /// <summary>
        /// The actor which should be implemented by the class
        /// </summary>
        Actor MeshActor { get; }

    }
}
