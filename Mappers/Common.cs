using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Mappers
{
    internal static class Common
    {



        internal static string GetProperty<TSource>(Expression<Func<TSource, object>> excludeProperty)
        {

            string[] result = excludeProperty.Body.ToString().Replace("(", "").Replace(")", "").Split(new char[] { '.' });
            string member = result[result.Length - 1];

            return member;


        }

        /// <summary>
        /// Filters the properties.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="excludeProperty">The property keys.</param>
        /// <returns></returns>
        internal static List<string> GetProperties<TSource>(params Expression<Func<TSource, object>>[] excludeProperty)
        {


            List<string> keys = new List<string>();


            excludeProperty.ToList().ForEach(expression =>
            {

                string[] result = expression.Body.ToString().Replace("(", "").Replace(")", "").Split(new char[] { '.' });
                string member = result[result.Length - 1];


                keys.Add(member);


            });


            return keys;


        }

        /// <summary>
        /// Targets the propery key.
        /// </summary>
        /// <param name="sourceMap">The source map.</param>
        /// <param name="sourceProperty">The source property.</param>
        /// <returns></returns>
        internal static string TargetProperyKey(SourceMappingKeys sourceMap, string sourceProperty)
        {
            string targetProperty = string.Empty;


            if (sourceMap != null && !string.IsNullOrEmpty(sourceProperty))
            {

                foreach (KeyValuePair<string, string> item in sourceMap.SourceMapping)
                {

                    if (Convert.ToString(item.Key).Equals(sourceProperty, StringComparison.OrdinalIgnoreCase))
                        targetProperty = item.Value.ToString();

                }


            }


            return string.IsNullOrEmpty(targetProperty) ? sourceProperty : targetProperty;


        }


    }
}
