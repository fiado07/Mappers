using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Mappers
{

    public class Mapper
    {


        public Mapper()
        {

        }


        /// <summary>
        /// Filters the properties.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="excludeProperty">The property keys.</param>
        /// <returns></returns>
        private List<string> FilterProperties<TSource>(params Expression<Func<TSource, object>>[] excludeProperty)
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
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="Ttarget">The type of the target.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="target">The target.</param>
        public void Map<TSource, Ttarget>(TSource source, Ttarget target)
        {


            Array.ForEach(source.GetType().GetProperties(),
                                x =>
                                {


                                    bool hasProperty = target.GetType().GetProperties().Any(i => i.Name == x.Name);

                                    if (hasProperty)
                                    {

                                        var value = source.GetType().GetProperty(x.Name).GetValue(source, null);


                                        if (value != null)
                                        {

                                            PropertyInfo TargetProperty = target.GetType().GetProperty(x.Name);

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
        public void Map<TSource, Ttarget>(TSource source, Ttarget target, params Expression<Func<TSource, object>>[] excludeProperty)
        {


            bool isPropertyKey = false;
            Func<string, bool> checkPropertyKey = key => FilterProperties(excludeProperty).Any(x => x == key);



            Array.ForEach(source.GetType().GetProperties(),
                                x =>
                                {

                                    bool hasProperty = target.GetType().GetProperties().Any(i => i.Name == x.Name);


                                    if (hasProperty)
                                    {

                                        var value = source.GetType().GetProperty(x.Name).GetValue(source, null);


                                        isPropertyKey = checkPropertyKey.Invoke(x.Name);


                                        if (value != null && !isPropertyKey)
                                        {

                                            PropertyInfo TargetProperty = target.GetType().GetProperty(x.Name);

                                            TargetProperty.SetValue(target, value, null);


                                        }


                                    }


                                });


        }



    }
}

