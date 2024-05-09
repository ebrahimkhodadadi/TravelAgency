﻿using System.Text.RegularExpressions;

namespace TravelAgency.Domain.Common.Utilities;

public static class RegexUtilities
{
    public static bool NotMatch(this Regex regex, string value)
    {
        return regex.IsMatch(value) is false;
    }
}