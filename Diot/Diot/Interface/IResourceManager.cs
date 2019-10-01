namespace Diot.Interface
{
    public interface IResourceManager
    {
        #region Methods

        string GetString(string key);

        object GetResource(string key);

        #endregion
    }
}