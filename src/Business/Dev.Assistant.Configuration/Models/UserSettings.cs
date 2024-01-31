using System.Configuration;

namespace Dev.Assistant.Configuration.Models;

// TODO: Enhance & refactor the code.

/// <summary>
/// Represents user settings for the application.
/// </summary>
public class UserSettings : ApplicationSettingsBase
{
    /// <summary>
    /// Gets or sets the startup application identifier.
    /// 0 - No app, user must start from the menu each time they run the app.
    /// 1 - Reviewme app.
    /// 2 - Reader app.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int StartupApp
    {
        get => (int)this["StartupApp"];
        set
        {
            this["StartupApp"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the identifier for 'WrittenByIG'.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string WrittenByIG
    {
        get => (string)this["WrittenByIG"];
        set
        {
            this["WrittenByIG"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the path to the MicroService.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string MicroServicePath
    {
        get => (string)this["MicroServicePath"];
        set
        {
            this["MicroServicePath"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the path to the Utilities Service.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string UtilitiesServicePath
    {
        get => (string)this["UtilitiesServicePath"];
        set
        {
            this["UtilitiesServicePath"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the path folder for IG.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string IGPathFolder
    {
        get => (string)this["IGPathFolder"];
        set => this["IGPathFolder"] = value;
    }

    /// <summary>
    /// Gets or sets the last input path.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string LastPathInput
    {
        get => (string)this["LastPathInput"];
        set
        {
            this["LastPathInput"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the log mode.
    /// 1 - Debug (including Trace).
    /// Otherwise, not included.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public int LogMode
    {
        get => (int)this["LogMode"];
        set => this["LogMode"] = value;
    }

    /// <summary>
    /// Gets or sets the token response object, serialized to JSON format.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public TokenResponse Token
    {
        get => (TokenResponse)this["Token"];
        set
        {
            this["Token"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the username for NicHq.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string NicHqUsername
    {
        get => (string)this["NicHqUsername"];
        set
        {
            this["NicHqUsername"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the search criteria for MyWork.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int MyWorkSearchCriteria
    {
        get => (int)this["MyWorkSearchCriteria"];
        set
        {
            this["MyWorkSearchCriteria"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the criteria for RFeature.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int RFeatureCriteria
    {
        get => (int)this["RFeatureCriteria"];
        set
        {
            this["RFeatureCriteria"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the selected option for RIG.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int RIGSelectedOption
    {
        get => (int)this["RIGSelectedOption"];
        set
        {
            this["RIGSelectedOption"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the project type option for RIG.
    /// 1 - Api.
    /// 2 - Micro.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int RIGProjectTypeOption
    {
        get => (int)this["RIGProjectTypeOption"];
        set
        {
            this["RIGProjectTypeOption"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether the RNdb open file check option is enabled.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool RNdbOpenFileCheckOption
    {
        get => (bool)this["RNdbOpenFileCheckOption"];
        set
        {
            this["RNdbOpenFileCheckOption"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether to list all employees in PList.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool PListAllEmp
    {
        get => (bool)this["PListAllEmp"];
        set
        {
            this["PListAllEmp"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the search criteria for staff.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("")]
    public string StaffSearchBy
    {
        get => (string)this["StaffSearchBy"];
        set
        {
            this["StaffSearchBy"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the selected option for RConverter.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int RConverterSelectedOption
    {
        get => (int)this["RConverterSelectedOption"];
        set
        {
            this["RConverterSelectedOption"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets the selected option for RCompare.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("0")]
    public int RCompareSelectedOption
    {
        get => (int)this["RCompareSelectedOption"];
        set
        {
            this["RCompareSelectedOption"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether active items are excluded.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool ExcludeActive
    {
        get => (bool)this["ExcludeActive"];
        set
        {
            this["ExcludeActive"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether to retrieve the developer's name.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool GetDevName
    {
        get => (bool)this["GetDevName"];
        set
        {
            this["GetDevName"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether to check for IG in attachments.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool CheckIgInAttachments
    {
        get => (bool)this["CheckIgInAttachments"];
        set
        {
            this["CheckIgInAttachments"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether the content was copied.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool Copied
    {
        get => (bool)this["Copied"];
        set
        {
            this["Copied"] = value;
            Save();
        }
    }

    /// <summary>
    /// Gets or sets whether to exclude items under development.
    /// </summary>
    [UserScopedSetting()]
    [DefaultSettingValue("false")]
    public bool ExcludeUnderDev
    {
        get => (bool)this["ExcludeUnderDev"];
        set
        {
            this["ExcludeUnderDev"] = value;
            Save();
        }
    }

    ///// <summary>
    /////
    ///// </summary>
    //[UserScopedSetting()]
    //[DefaultSettingValue("")]
    //public string Pat
    //{
    //    get => StringCipher.Decrypt((string)this["Pat"]);
    //    set
    //    {
    //        this["Pat"] = StringCipher.Encrypt(value);
    //        //Save();
    //    }
    //}
}