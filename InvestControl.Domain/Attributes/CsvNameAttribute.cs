using System;

namespace InvestControl.Domain.Attributes;

public class CsvNameAttribute : Attribute
{
    public string ColumnName { get; protected set; }

    public CsvNameAttribute(string columnName)
    {
        ColumnName = columnName;
    }
}