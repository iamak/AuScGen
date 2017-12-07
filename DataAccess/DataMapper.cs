// ***********************************************************************
// <copyright file="DataMapper.cs" company="EDMC">
//     Copyright © EDMC, All Rights Reserved.
// </copyright>
// <summary>DataMapper class</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EDMC.DataAccess
{
    /// <summary>
    /// DataMapper
    /// </summary>
    public class DataMapper
    {
        /// <summary>
        /// Creates the list.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        internal static List<T> CreateList<T>(DbDataReader reader)
        {
            List<T> list = new List<T>();
            Func<object[], T> converter = GetConverter<T>((IDataRecord)reader, 0);
            while (reader.Read())
                list.Add(converter(GetValues((IDataRecord)reader)));
            return list;
        }

        /// <summary>
        /// Gets the converter.
        /// </summary>
        /// <typeparam name="T">Generic type parameter</typeparam>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        internal static Func<object[], T> GetConverter<T>(IDataRecord record, int index)
        {
            Type t = typeof(T);
            if (1 == record.FieldCount - index && record.GetFieldType(index) == t)
                return (Func<object[], T>)(o => (T)o[index]);
            //if (typeof(object[]) == t)
            //{
            //    if (index == 0)
            //        return (Func<object[], T>)(o => (T)o);
            //    return (Func<object[], T>)(o =>
            //    {
            //        object[] objArray = new object[o.Length - index];
            //        Array.Copy((Array)o, index, (Array)objArray, 0, objArray.Length);
            //        return (T)objArray;
            //    });
            //}
            ConstructorInfo ci = GetConstructor(t, record, index);
            if (index == 0)
                return (Func<object[], T>)(o => (T)ci.Invoke(o));
            return (Func<object[], T>)(o =>
            {
                object[] parameters = new object[o.Length - index];
                Array.Copy((Array)o, index, (Array)parameters, 0, parameters.Length);
                return (T)ci.Invoke(parameters);
            });
        }

        /// <summary>
        /// Gets the constructor.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="record">The record.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        /// <exception cref="System.Data.DataException"></exception>
        internal static ConstructorInfo GetConstructor(Type t, IDataRecord record, int index)
        {
            ConstructorInfo constructorInfo = Array.Find<ConstructorInfo>(t.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), (Predicate<ConstructorInfo>)(c =>
            {
                ParameterInfo[] parameters = c.GetParameters();
                if (parameters.Length != record.FieldCount - index)
                    return false;
                for (int index1 = 0; index1 < parameters.Length; ++index1)
                {
                    Type enumType = parameters[index1].ParameterType;
                    Type fieldType = record.GetFieldType(index1 + index);
                    Type[] genericArguments;
                    if (enumType.IsGenericType && typeof(Nullable<>) == enumType.GetGenericTypeDefinition() && ((genericArguments = enumType.GetGenericArguments()) != null && genericArguments.Length > 0))
                        enumType = genericArguments[0];
                    if (!enumType.IsAssignableFrom(fieldType) && (!enumType.IsEnum || !Enum.GetUnderlyingType(enumType).IsAssignableFrom(fieldType)))
                        return false;
                }
                return true;
            }));
            if ((ConstructorInfo)null != constructorInfo)
                return constructorInfo;
            throw new DataException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "Unable to find constructor for type {0}. Available constructors do not match source. Source contains fields of type ({1}).", new object[2]
            {
                (object) t.Name,
                (object) string.Join(", ", Enumerable.ToArray<string>(Enumerable.Select<Type, string>((IEnumerable<Type>) GetTypes(record), (Func<Type, string>) (ty => ty.Name))))
            }));
        }

        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns></returns>
        internal static Type[] GetTypes(IDataRecord record)
        {
            Type[] typeArray = new Type[record.FieldCount];
            for (int i = 0; i < record.FieldCount; ++i)
                typeArray[i] = record.GetFieldType(i);
            return typeArray;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <returns></returns>
        internal static object[] GetValues(IDataRecord record)
        {
            object[] values = new object[record.FieldCount];
            record.GetValues(values);
            for (int index = 0; index < values.Length; ++index)
            {
                if (DBNull.Value == values[index])
                    values[index] = (object)null;
            }
            return values;
        }       
    }
}
