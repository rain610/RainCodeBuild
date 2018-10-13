using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Common
{
    public static class ListToTableHelper
    {
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            var dt = new DataTable();
            try
            {
                //检查泛型实体是否为空
                if (entitys == null || entitys.Count <= 0)
                {
                    return dt;
                }
                var entityType = entitys[0].GetType();
                var entityProperties = entityType.GetProperties();
                for (var i = 0; i < entityProperties.Length; i++)
                {
                    dt.Columns.Add(entityProperties[i].Name);
                }
                //将所有entity添加到DataTable中
                foreach (object entity in entitys)
                {
                    //检查所有的的实体都为同一类型
                    if (entity.GetType() != entityType)
                    {
                        throw new Exception("要转换的集合元素类型不一致");
                    }
                    var entityValues = new object[entityProperties.Length];
                    for (var i = 0; i < entityProperties.Length; i++)
                    {
                        entityValues[i] = entityProperties[i].GetValue(entity, null);
                    }
                    dt.Rows.Add(entityValues);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return dt;
        }
    }
}
