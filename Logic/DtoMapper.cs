using System.Reflection;
namespace Rollcall.Logic
{
    public class Mapping
    {
        public string InputType { get; set; }
        public string OutputType { get; set; }
        public Dictionary<string, string> PropertyMap { get; set; }
        public Mapping()
        {
            PropertyMap = new Dictionary<string, string>();
            InputType = "";
            OutputType = "";
        }
    }
    public class DtoMapper
    {
        private List<Mapping> _mappings;

        public DtoMapper() { _mappings = new List<Mapping>(); }
        public void addMapping<T, Q>()
        {
            var mapping = new Mapping();
            Type inputType = typeof(T);
            Type outputType = typeof(Q);
            mapping.InputType = inputType.Name;
            mapping.OutputType = outputType.Name;

            var outputPropertiesList = outputType.GetProperties();

            foreach (var inputProperty in inputType.GetProperties())
            {
                var outputProperty = outputPropertiesList.SingleOrDefault(prop => (prop.Name == inputProperty.Name));
                if (outputProperty != null && outputProperty.PropertyType.Equals(inputProperty.PropertyType))
                {
                    Console.WriteLine("Mapping added: "+outputProperty.Name);
                    mapping.PropertyMap.Add(outputProperty.Name, inputProperty.Name);
                }
            }
            _mappings.Add(mapping);
        }
        public T Map<T, Q>(Q q) where T : new()
        {
            Type inputType = typeof(Q);
            Type outputType = typeof(T);
            var mapping = _mappings.SingleOrDefault(map => map.InputType == inputType.Name && map.OutputType == outputType.Name);
            if (mapping == null)
            {
                throw new InvalidDataException($"No existing mapping from: {inputType.Name} to {outputType.Name}");
            }
            var result = new T();
            foreach (var propertyNames in mapping.PropertyMap)
            {
                var prop = outputType.GetProperty(propertyNames.Value);
                var propValue = prop.GetValue(result);
                var newValue = inputType.GetProperty(propertyNames.Key).GetValue(q);

                propValue = newValue;
                prop.SetValue(result,propValue);
            }
            return result; 
        }
    }
}