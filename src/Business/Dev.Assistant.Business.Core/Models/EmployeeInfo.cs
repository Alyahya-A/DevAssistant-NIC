using Newtonsoft.Json;

namespace Dev.Assistant.Business.Core.Models;

public class EmployeeInfo
{
    [JsonProperty("user_name")]
    public string Username { get; set; }

    [JsonProperty("full_name")]
    public string FullName { get; set; }

    [JsonProperty("postion_name")]
    public string PositionName { get; set; }

    [JsonProperty("phone_ext")]
    public string PhoneExt { get; set; }

    [JsonProperty("email_address")]
    public string Email { get; set; }

    [JsonProperty("org_section")]
    public string OrgSectionName { get; set; }

    [JsonProperty("current_dept_name")]
    public string CurrentDeptName { get; set; }

    [JsonProperty("org_manager_name")]
    public string ManagerName { get; set; }
}

public class EmployeeInfoOld
{
    public string EmployeeFullName { get; set; }

    public string EmployeeCurrentDeptName { get; set; }

    public string EmployeePhone { get; set; }

    public string EmployeeEmail { get; set; }
}