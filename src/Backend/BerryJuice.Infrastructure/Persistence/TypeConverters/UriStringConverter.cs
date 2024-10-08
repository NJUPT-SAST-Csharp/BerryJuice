﻿using System.Data;
using Dapper;

namespace BerryJuice.Infrastructure.Persistence.TypeConverters;

internal class UriStringConverter : SqlMapper.TypeHandler<Uri?>
{
    public override void SetValue(IDbDataParameter parameter, Uri? value)
    {
        parameter.Value = value?.ToString();
    }

    public override Uri? Parse(object value)
    {
        if (String.IsNullOrWhiteSpace(value as string))
        {
            return null;
        }

        return new Uri((string)value);
    }
}
