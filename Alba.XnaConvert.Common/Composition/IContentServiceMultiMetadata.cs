namespace Alba.XnaConvert.Common
{
    public interface IContentServiceMultiMetadata
    {
        string[] Name { get; }
        string[] Version { get; }
        bool[] IsPublic { get; }
    }
}