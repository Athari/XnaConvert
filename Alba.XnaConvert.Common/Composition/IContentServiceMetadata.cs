namespace Alba.XnaConvert.Common
{
    public interface IContentServiceMetadata
    {
        string Name { get; }
        string Version { get; }
        bool IsPublic { get; }
    }
}