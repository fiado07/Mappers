using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mappers
{

    public static class Mapper
    {

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="Ttarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public static void Map<TSource, Ttarget>(this TSource source, Ttarget target, SourceMappingKeys sourceMap = null)
        {

            string targetProperty = string.Empty;
            bool hasProperty = false;

            Array.ForEach(source.GetType().GetProperties(),
                                  x =>
                                  {

                                      targetProperty = Common.TargetProperyKey(sourceMap, x.Name);


                                      hasProperty = target.GetType().GetProperties().Any(i => i.Name.Equals(targetProperty, StringComparison.OrdinalIgnoreCase));


                                      if (hasProperty)
                                      {

                                          var value = source.GetType().GetProperty(x.Name).GetValue(source, null);


                                          if (value != null)
                                          {

                                              PropertyInfo TargetProperty = target.GetType().GetProperties().FirstOrDefault(k => k.Name.Equals(targetProperty, StringComparison.OrdinalIgnoreCase));

                                              TargetProperty.SetValue(target, value, null);


                                          }


                                      }


                                  });


        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="Ttarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        /// <param name="excludeProperty">The property keys.</param>
        public static void Map<TSource, Ttarget>(this TSource source, Ttarget target, SourceMappingKeys sourceMap = null, params Expression<Func<TSource, object>>[] excludeProperty)
        {

            string targetProperty = string.Empty;
            bool hasProperty = false;
            bool isPropertyKey = false;
            Func<string, bool> checkProperty = key => Common.GetProperties(excludeProperty).Any(x => x == key);


            Array.ForEach(source.GetType().GetProperties(),
                                x =>
                                {

                                    targetProperty =Common.TargetProperyKey(sourceMap, x.Name);


                                    hasProperty = target.GetType().GetProperties().Any(i => i.Name.Equals(targetProperty, StringComparison.OrdinalIgnoreCase));


                                    if (hasProperty)
                                    {

                                        var value = source.GetType().GetProperty(x.Name).GetValue(source, null);


                                        isPropertyKey = checkProperty.Invoke(x.Name);


                                        if (value != null && !isPropertyKey)
                                        {

                                            PropertyInfo TargetProperty = target.GetType().GetProperties().FirstOrDefault(k => k.Name.Equals(targetProperty, StringComparison.OrdinalIgnoreCase));

                                            TargetProperty.SetValue(target, value, null);


                                        }


                                    }


                                });


        }

        /// <summary>
        /// Maps datatable source to object.
        /// </summary>
        /// <typeparam name="Ttarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IEnumerable<Ttarget> Map<Ttarget>(this DataTable source, SourceMappingKeys sourceMap = null) where Ttarget : new()
        {

            List<string> columnNames = new List<string>();
            DataTable dataTableSchemer = source.Clone();
            string targetProperty = string.Empty;
            Ttarget target = new Ttarget();


            // Get columns
            columnNames = dataTableSchemer.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToList();


            foreach (DataRow datarow in source.Rows)
            {


                // --:: set values

                columnNames.ForEach(x =>
                {


                    targetProperty = Common.TargetProperyKey(sourceMap, x);


                    // get properties and set new values 
                    Array.ForEach(target.GetType().GetProperties(),
                        (_propInfo) =>
                        {

                            if (string.Equals(_propInfo.Name.ToString(), targetProperty, StringComparison.OrdinalIgnoreCase))
                            {

                                var valueObject = datarow[x];


                                if (_propInfo.PropertyType == typeof(DateTime))
                                {

                                    // set value 
                                    if (!string.IsNullOrEmpty(Convert.ToString(valueObject))) _propInfo.SetValue(target, Convert.ToDateTime(valueObject), null);

                                }
                                else
                                {
                                    // set value 
                                    if (!string.IsNullOrEmpty(Convert.ToString(valueObject))) _propInfo.SetValue(target, valueObject, null);
                                }


                            }

                        });


                });

                yield return target;


            }



        }


    }
}

