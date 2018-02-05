namespace AssistidCollector1.Interfaces
{
    public interface InterfaceContentLocation
    {
        /// <summary>
        /// Get base location for assets
        /// </summary>
        /// <returns></returns>
        string GetBaseLocation();

        /// <summary>
        /// Get personal location for assets
        /// </summary>
        /// <returns></returns>
        string GetPersonalLocation();
    }
}
