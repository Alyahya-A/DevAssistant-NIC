using Dev.Assistant.Business.Core.Extensions;
using Dev.Assistant.Business.Core.Models;
using Dev.Assistant.Business.Core.Utilities;
using Dev.Assistant.Configuration;

namespace Dev.Assistant.Business.Core.Services;

public static class ValidationService
{
    private const int allowedLimit = 10;

    /// <summary>
    ///
    /// </summary>
    /// <param name="servicesCount"></param>
    /// <param name="log"></param>
    /// <returns></returns>
    public static bool ValidateServiceLimit(int servicesCount, LogArgs log)
    {
        if (servicesCount > 1)
        {
            if (servicesCount > allowedLimit)
            {
                log.LogError($"Unable to proceed bacause there are {servicesCount} services found, exceeding the limit of {allowedLimit}");
                return false;
            }

            var dialogInfo = new DevDialogInfo
            {
                Title = $"Mulitple services found:",
                Message = $"We found {servicesCount} services. Do you want to continue and Check Rules for all servcies?",
                Buttons = MessageBoxButtons.OK,
                BoxIcon = MessageBoxIcon.Question
            };

            DialogResult result = DevDialog.Show(dialogInfo);

            if (result == DialogResult.No)
            {
                log.LogError($"Stop checking...");

                return false;
            }
        }

        return true;
    }


    public static bool IsExcludedMicro(string projectName)
    {
        if (!projectName.ToLower().StartsWith("micro"))
            return false;

        return Consts.ExcludedMicros.Contains(projectName);
    }

    /// <summary>
    /// Validate if datatype is primitive type or non-user classes such as string and DateTime.
    /// </summary>
    /// <param name="dataType"></param>
    /// <param name="excludeDates">Exclude dates PrimitiveTypes, if dataType is date this means it is not primitive type</param>
    /// <param name="avoidList"></param>
    /// <returns></returns>
    public static bool IsPrimitiveDatatype(string dataType, bool excludeDates = false, bool avoidList = true)
    {
        if (string.IsNullOrWhiteSpace(dataType))
            return false;

        if (avoidList)
            dataType = dataType.Replace("?", "").Replace("[]", "").Replace("list<", "").Replace(">", "");

        if (excludeDates)
        {
            List<string> dateTypes = new() { "date", "datetimeoffset", "datetime", "timeonly", "dateonly", "time" };

            if (dateTypes.Contains(dataType.ToLower()))
                return false; // It is not primitive type if type was date and excludeDates
        }

        if (Consts.PrimitiveTypes.Contains(dataType.ToLower()))
            return true;

        return false;
    }


    public static bool IsArabic(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return value.IsArabic();
    }

    //public static bool IsExcludedContracts(string projectName)
    //{
    //    return Consts.ExcludedContracts.Contains(projectName);
    //}
}