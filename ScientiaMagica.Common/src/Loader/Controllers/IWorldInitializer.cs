namespace ScientiaMagica.Common.Loader.Controllers {
    /// <summary>
    /// Should be used for systems that need to do init processing
    /// </summary>
    public interface IWorldInitializer {
        InitFlags InitFlags { get; }
        int InitPriority { get; }
        void Init();
    }
}