using System.Reflection;
using System.Collections;

namespace Rollcall.Helpers
{
    //TODO
    public class Mapper
    {
        public IEnumerable<Tuple<PropertyInfo, PropertyInfo>> CreateMapping<T, Q>()
        {
            var dstFields = typeof(T).GetProperties();
            var sourceType = typeof(Q);

            var commonFields = new List<Tuple<PropertyInfo, PropertyInfo>>();

            foreach (var dstField in dstFields)
            {
                var srcField = sourceType.GetProperty(dstField.Name);
                if (srcField is not null && srcField.PropertyType.Equals(dstField.PropertyType))
                {
                    commonFields.Add(new Tuple<PropertyInfo,PropertyInfo>(dstField, srcField));
                }
            }
            return commonFields;
        }
        public T MapProperties<T, Q>(Q q) where T : new()
        {
            var result = new T();
            var mapping = CreateMapping<T, Q>();
            foreach (Tuple<PropertyInfo,PropertyInfo> map in mapping)
            {
                map.Item1.SetValue(result, map.Item2.GetValue(q));
            }
            return result;
        }
    }
}