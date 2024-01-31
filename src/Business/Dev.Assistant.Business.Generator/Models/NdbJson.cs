namespace Dev.Assistant.Business.Generator.Models;

public class NdbJson
{
    public Onboardingrequest onBoardingRequest { get; set; }
}

public class Onboardingrequest
{
    public Header header { get; set; }
    public Body body { get; set; }
}

public class Header
{
    public string transaction_date { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
    public Guid transaction_id { get; set; } = Guid.NewGuid();
}

public class Body
{
    public string api_name_tech { get; set; }
    public string api_name { get; set; }
    public string api_name_ar { get; set; }
    public string version { get; set; } = "1.0";
    public string short_description { get; set; }
    public string long_description { get; set; }
    public string long_description_ar { get; set; }
    public string short_description_ar { get; set; }
    public string created_date { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
    public string last_updated_date { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss:fff}";
    public string api_owner_code { get; set; } = "GW02";
    public string api_load_type_id { get; set; } = "size02";
    public string data_type_id { get; set; } = "data02";
    public string api_type_id { get; set; } = "typ01";
    public string industry_id { get; set; } = "ind04";
    public string category_id { get; set; } = "cat02";
    public string onboard_action { get; set; } = "create";
    public List<string> tags_ar { get; set; }
    public List<string> tags { get; set; }
    public List<string> security_ids { get; set; } = new() { "2" };
    public List<string> data_providers { get; set; } = new() { "1" };
    public List<string> operations { get; set; }

    public List<Documentation> documentation { get; set; } = new()
    {
        new() { type = "REST", content = "data:application/octet-stream;base64,", file_name = "Swagger.json" }
    };

    public List<Business_Fields_Input> business_fields_input { get; set; }
    public List<Business_Fields_Output> business_fields_output { get; set; }
}

public class Documentation
{
    public string type { get; set; }
    public string content { get; set; }
    public string file_name { get; set; }
}

public class Business_Fields_Input
{
    public string key_en { get; set; }
    public string value_en { get; set; }
    public string key_ar { get; set; }
    public string value_ar { get; set; }
}

public class Business_Fields_Output
{
    public string key_en { get; set; }
    public string value_en { get; set; }
    public string key_ar { get; set; }
    public string value_ar { get; set; }
    public string data_source_id { get; set; } = "1";
}