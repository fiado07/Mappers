using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Mappers
{
    public class SourceMappingKeys
    {


        public Dictionary<string, string> SourceMapping { get; private set; }


        public SourceMappingKeys()
        {

            SourceMapping = new Dictionary<string, string>();


        }
        public SourceMappingKeys(string sourceKey, string targetKey)
        {

            SourceMapping = new Dictionary<string, string>();

            SourceMapping.Add(sourceKey, targetKey);


        }
               
              
        public void Add(string sourceKey, string targetKey)
        {

            SourceMapping.Add(sourceKey, targetKey);


        }

        public void Add<TSource, Ttarget>(Expression<Func<TSource, object>> sourceKey, Expression<Func<Ttarget, object>> targetKey)
        {

            SourceMapping.Add(Common.GetProperty(sourceKey), Common.GetProperty(targetKey));


        }




    }
}
