﻿using CRUD.ManagementUser.Application.Common.Constants;
using CRUD.ManagementUser.Application.Common.Formats;

namespace CRUD.ManagementUser.Application.Common.Extensions;

public static class DateTimeExtensions
{
    public static string ToShortDateDisplayText(this DateTime dateTime)
    {
        return dateTime.ToString(DateTimeFormats.DD_MMM_YYYY);
    }

    public static string ToShortDateDisplayText(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return DefaultTextFor.Dash;
        }

        return dateTime.Value.ToString(DateTimeFormats.DD_MMM_YYYY);
    }

    public static string ToLongDateTimeDisplayText(this DateTime dateTime)
    {
        return dateTime.ToString(DateTimeFormats.DD_MMMM_YYYY_HH_MM_SS);
    }

    public static string ToLongDateTimeDisplayText(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return DefaultTextFor.Dash;
        }

        return dateTime.Value.ToString(DateTimeFormats.DD_MMMM_YYYY_HH_MM_SS);
    }

    public static string ToCompleteDateTimeDisplayText(this DateTime dateTime)
    {
        return dateTime.ToString(DateTimeFormats.DD_MMMM_YYYY_HH_MM_SS_ZZZ);
    }

    public static string ToCompleteDateTimeDisplayText(this DateTime? dateTime)
    {
        if (!dateTime.HasValue)
        {
            return DefaultTextFor.Dash;
        }

        return dateTime.Value.ToString(DateTimeFormats.DD_MMMM_YYYY_HH_MM_SS_ZZZ);
    }
}
