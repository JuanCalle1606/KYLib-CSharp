
using KYLib.Interfaces;
using Newtonsoft.Json;


namespace KYLib.Example
{
    public class ExampleObject : INameable
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public float Length { get; set; }

        public bool IsMale { get; set; }
    }
}