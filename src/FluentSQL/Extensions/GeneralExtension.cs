﻿using FluentSQL.Helpers;
using FluentSQL.Models;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace FluentSQL.Extensions
{
    public static class GeneralExtension
    {
        internal static IEnumerable<PropertyOptions> GetPropertyQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            selectMember.NullValidate(ErrorMessages.ParameterNotNull, nameof(selectMember));
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop).ToArray();
        }

        internal static IEnumerable<ColumnAttribute> GetColumnsQuery(this ClassOptions options, IEnumerable<string> selectMember)
        {
            return (from prop in options.PropertyOptions
                    join sel in selectMember on prop.PropertyInfo.Name equals sel
                    select prop.ColumnAttribute).ToArray();
        }

        internal static (ClassOptions Options, IEnumerable<MemberInfo> MemberInfos) GetOptionsAndMembers<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            IEnumerable<MemberInfo> memberInfos = expression.GetMembers();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return (options, memberInfos);
        }

        internal static (ClassOptions Options, MemberInfo MemberInfos) GetOptionsAndMember<T, TProperties>(this Expression<Func<T, TProperties>> expression)
        {
            expression.NullValidate(ErrorMessages.ParameterNotNull, nameof(expression));

            MemberInfo memberInfos = expression.GetMember();
            ClassOptions options = ClassOptionsFactory.GetClassOptions(typeof(T));

            return (options, memberInfos);
        }

        internal static void ValidateMemberInfos(this IEnumerable<MemberInfo> memberInfos, string message)
        {
            if (!memberInfos.Any())
            {
                throw new InvalidOperationException(message);
            }
        }

        internal static PropertyOptions ValidateMemberInfo(this MemberInfo memberInfo, ClassOptions options)
        {
            PropertyOptions? result = options.PropertyOptions.FirstOrDefault(x => x.PropertyInfo.Name == memberInfo.Name);

            if (result == null)
            {
                throw new InvalidOperationException($"Could not find property {memberInfo.Name} on type {options.Type.Name}");
            }

            return result;
        }

        internal static object GetValue(this PropertyOptions options, object entity)
        {
            return options.PropertyInfo.GetValue(entity, null) ?? DBNull.Value;
        }

        public static IEnumerable<IDataParameter> GetParameters<T, TDbConnection>(this IQuery query, 
            IDatabaseManagement<TDbConnection> databaseManagment) where T : class, new()
        {
            Queue<ParameterDetail> parameters = new();
            if (query.Criteria != null)
            {
                foreach (var item in query.Criteria.Where(x => x.ParameterDetails is not null))
                {
                    foreach (var item2 in item.ParameterDetails)
                    {
                        parameters.Enqueue(item2);
                    }
                }
            }

            return databaseManagment.Events.GetParameter<T>(parameters);
        }
    }
}
