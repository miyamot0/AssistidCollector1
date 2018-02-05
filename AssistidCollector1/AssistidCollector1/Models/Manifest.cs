using System.Collections.Generic;

namespace AssistidCollector1.Models
{
    /// <summary>
    /// Class for deserializing JSON
    /// </summary>
    public class Manifest
    {
        public int Iteration;
        public List<Items> Tasks;
    }
}
