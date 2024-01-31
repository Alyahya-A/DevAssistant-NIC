using Dev.Assistant.Business.Converter.Models;
using Dev.Assistant.Business.Core.DevErrors;
using Dev.Assistant.Business.Core.Extensions;
using Serilog;
using System.Text;
using System.Text.RegularExpressions;

// Error code starts with 4000

namespace Dev.Assistant.Business.Converter.Services;

/// <summary>
/// Service class for cleaning and processing SQL queries.
/// </summary>
public static class SqlConvertService
{
    #region Public Method

    /// <summary>
    /// Cleans the input SQL query by removing certain patterns and formats.
    /// </summary>
    /// <param name="query">The SQL query to be cleaned.</param>
    /// <param name="isThrow">Flag indicating whether to throw exceptions for invalid queries. Default is true.</param>
    /// <returns>The cleaned SQL query.</returns>
    /// <exception cref="DevAssistantException">Thrown when an error occurs during query cleaning.</exception>
    public static string CleanSqlOld(string query, bool isThrow = true)
    {
        // Log the method call
        Log.Logger.Information("CleanSqlOld Called");

        // Check if the input query is null or whitespace
        if (string.IsNullOrWhiteSpace(query))
        {
            throw DevErrors.Converter.E4001InputIsNull;
        }
        // Check if the query contains necessary SQL keywords
        else if (!query.ToLower().Contains("from ") || !query.ToLower().Contains("select "))
        {
            throw DevErrors.Converter.E4002InvalidSqlStatement;
        }

        // List of strings to be removed from the SQL query
        string[] listStr = {
                "SqlStat.Append(\"",
                "SqlStat.AppendLine(\"",
                "SqlState.Append(\"",
                "SqlState.AppendLine(\"",
                "cmdtxt$.Append(\"",
                "cmdtxt$.AppendLine(\"",
                "cmdText$ .Append(\"",
                "cmdText$.AppendLine(\"",
                "string cmdtxt$ = \"",
                "string SqlTxt$ = \"",
                "string query$ = \"",
                "cmdtxt$ = \"",
                "cmdtxt$ += \"",
                "cmd.CommandText = \"",
                "cmd.CommandText += \"",
                "SqlTxt$ += \"",
                "query$ += \"",
                "SqlTxt$ = \"",
                "Sqltxt$ += \"",
                "query$ = \"",
                "query$.AppendLine(\"",
                "query$.Append(\"",
                "\");",
                ".Append(\"",
                ".AppendLine(\"",
                "\";",
                "+",
                "SqlState = \"",
                "SqlState += \"",
            };

        string sql = query;
        bool isContainSql = false;

        try
        {
            foreach (string str in listStr)
            {
                string tempStr = str;

                for (int i = 0; i < 10; i++)
                {
                    if (i == 0)
                    {
                        tempStr = str.Replace("$", "");
                    }
                    else
                    {
                        tempStr = str.Replace("$", i.ToString());
                    }

                    if (sql.Contains(tempStr) && !isContainSql && !str.Equals("\";") && !str.Equals("+"))
                    {
                        isContainSql = true;
                    }

                    sql = sql.Replace(tempStr, "");
                }
            }

            // If no SQL statement is found and throwing exceptions is enabled, throw an exception
            if (!isContainSql && isThrow)
            {
                throw DevErrors.Converter.E4003NoSqlStatementFound;
            }

            RegexOptions options = RegexOptions.None;

            // Remove extra spaces and tabs from the cleaned SQL query
            Regex regex = new("[ ]{2,}", options);
            sql = regex.Replace(sql.Replace("\t", " "), "");

            // If throwing exceptions is enabled, further process the SQL query
            if (isThrow)
            {
                string[] lines = sql.Split(Convert.ToChar("\n"));

                string cleanedSql = string.Empty;

                foreach (string s in lines)
                {
                    string tempStr = s.Trim();

                    // VB
                    if (tempStr.EndsWith("\""))
                    {
                        tempStr = tempStr[..tempStr.LastIndexOf("\"")];
                    }

                    if (tempStr.Contains("//"))
                    {
                        tempStr = tempStr[..tempStr.IndexOf("//")];
                    }

                    cleanedSql += tempStr + "\n";

                    Console.WriteLine("Line: " + cleanedSql);
                }

                return cleanedSql;
            }

            //string[] lines = sql.Split(Convert.ToChar("\n"));

            //string cleanedSql = string.Empty;

            //foreach (string s in lines)
            //{
            //    string tempStr = s.Trim();

            //    if (tempStr.Contains("//"))
            //    {
            //        tempStr = tempStr.Substring(0, tempStr.IndexOf("//"));
            //    }

            //    cleanedSql += tempStr.Trim() + "\n";

            //}

        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in CleanSql");

            throw;
        }

        return sql;
    }

    /// <summary>
    /// This service is used to clean the SQL statement by removing variable names and spaces. etc.
    /// </summary>
    /// <param name="query">SQL statement</param>
    /// <param name="options">Clean SQL options</param>
    /// <returns>Cleaned SQL statement</returns>
    public static string CleanSql(string query, SqlConvertOptions options = null)
    {
        // Log that CleanSql method has been called
        Log.Logger.Information("CleanSql Called");

        // If options are not provided, create a new instance
        options ??= new SqlConvertOptions();

        // List of allowed SQL keywords
        List<string> allowedWords = new()
        {
            "from ",
            "select ",
            "where ",
            "join ",
            "order ",
            "group ",
        };

        if (string.IsNullOrWhiteSpace(query))
        {
            throw DevErrors.Converter.E4101InputIsNullOrEmpty;
        }
        else if (!allowedWords.Any(w => query.ToLower().Contains(w)))
        {
            // Throw an exception if the query is missing SQL keywords
            throw DevErrors.Converter.E4102InvalidSqlKeyword;
        }

        if (options.RemoveWhiteSpace)
        {
            RegexOptions regOptions = RegexOptions.None;
            Regex regex = new("[ ]{2,}", regOptions);

            //                                  "\t" means tap
            query = regex.Replace(query.Replace("\t", " "), " ");
        }

        // List to store the cleaned SQL
        var cleanedSql = new List<string>();

        // Split the query into lines
        string[] lines = query.Split(Convert.ToChar("\n"));

        int totalLines = 0;
        string commentSymbol = "NONE";

        try
        {
            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                // Check for newline or whitespace and handle accordingly
                if (line.Equals("\n"))
                {
                    cleanedSql.Add(Environment.NewLine);
                    totalLines++;

                    continue;
                }
                else if (string.IsNullOrWhiteSpace(line))
                {
                    cleanedSql.Add("");
                    totalLines++;

                    continue;
                }

                // Skip lines starting with "var " or containing "new SqlParameter"
                if (trimmedLine.StartsWith("var ") || trimmedLine.Contains("new SqlParameter"))
                {
                    continue;
                }

                // A copy of the original line for modification
                string tempLine = line;

                              // Handle comments
                //                        ||            C#              ||             VB              ||         Sql Server
                if (!options.IsSqlToCode && (tempLine.StartsWith("//") || tempLine.StartsWith("\'")) || tempLine.StartsWith("--"))
                {
                    if (options.RemoveComments)
                    {
                        // Skip the line if comments are to be removed
                        continue;
                    }
                    else
                    {
                        // Handle comment and update commentSymbol
                        commentSymbol = HandleComment(tempLine, options.CommentStyle, cleanedSql);
                        totalLines++;
                        continue;
                    }
                }

                // Handle SQL lines
                if (options.IsSqlToCode)
                {
                    // in case clean called by SqlToCode. this mean it is sql line. so added
                    cleanedSql.Add(tempLine);
                    totalLines++;
                    continue;
                }

                var firstIndex = tempLine.IndexOf("\"");
                var lastIndex = tempLine.LastIndexOf("\"");

                if (firstIndex == lastIndex)
                {
                    continue;
                }

                // check if the previous is comment. to remove first spaces from comment as we'll remove from templine
                if (!options.RemoveComments && !options.RemoveWhiteSpace && totalLines > 0 && cleanedSql[totalLines - 1].StartsWith(commentSymbol))
                {
                    // Remove leading spaces from comments if the previous line ended with a comment
                    cleanedSql[totalLines - 1] = $"{cleanedSql[totalLines - 1][..2]}{cleanedSql[totalLines - 1][(firstIndex + 4)..]}";
                }

                var length = lastIndex - firstIndex;

                // check if there is a comment at the end of the line "query (Statement)" // Comment
                var afterStatement = tempLine[lastIndex..].Trim();
                tempLine = tempLine.Substring(firstIndex + 1, length - 1);

                if (!options.IsSqlToCode && !options.RemoveComments && afterStatement.Contains("//"))
                {
                    // Convert // comments to -- in SQL lines
                    afterStatement = afterStatement[afterStatement.IndexOf("//")..].Replace("//", "--");
                    tempLine += $" {afterStatement}";
                }

                // Trim whitespace if specified in options
                if (options.RemoveWhiteSpace)
                {
                    tempLine = tempLine.Trim();
                }

                cleanedSql.Add(tempLine);

                totalLines++;
            }

            if (cleanedSql.Count == 0)
            {
                throw DevErrors.Converter.E4104CannotCleanQuery;
            }

            // Check if the query is already cleaned
            if (query.RemoveWhiteSpaces().Equals(cleanedSql.ToString().RemoveWhiteSpaces()))
            {
                throw DevErrors.Converter.E4103QueryAlreadyCleaned;
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in CleanSql");

            throw;
        }

        return string.Join(Environment.NewLine, cleanedSql);
    }

    /// <summary>
    /// Converts code to SQL statement.
    /// This method is used by Reviewme to generate a SQL statement from the provided code.
    /// </summary>
    /// <param name="code">The code to convert into a SQL statement</param>
    /// <param name="options">Options for cleaning SQL code</param>
    /// <returns>The generated SQL statement</returns>
    public static string CodeToSql(string code, SqlConvertOptions options = null)
    {
        Log.Logger.Information("CodeToSql Called");

        // If options are not provided, use default options
        options ??= new SqlConvertOptions { RemoveComments = false, CommentStyle = DevCommentType.Sql, RemoveWhiteSpace = true, IsSqlToCode = false };
        {
            // Clean the code to generate a SQL statement
            var query = CleanSql(code, options);

            return query;
        }
    }

    /// <summary>
    /// Converts SQL statement to C# code.
    /// This method is used to generate C# code that can execute the provided SQL statement.
    /// </summary>
    /// <param name="query">The SQL statement to convert into C# code</param>
    /// <param name="options">Options for cleaning SQL code</param>
    /// <returns>The generated C# code</returns>
    public static string SqlToCode(string query, SqlConvertOptions options = null)
    {
        Log.Logger.Information("SqlToCode Called");

        // If options are not provided, use default options
        options ??= new SqlConvertOptions { RemoveComments = false, RemoveWhiteSpace = false, CommentStyle = DevCommentType.CSharp };

        // Clean the SQL statement to prepare for conversion
        query = CleanSql(query, options);

        // Split the SQL statement into lines
        string[] lines = query.Split(Convert.ToChar("\n"));

        // Initialize a StringBuilder to store the generated C# code
        var cleanedSql = new StringBuilder();

        // Start with the initialization of cmdText StringBuilder
        cleanedSql.AppendLine("var cmdText = new StringBuilder();");

        var isFirst = true;

        try
        {
            foreach (var line in lines)
            {
                if (line.Equals("\n") || string.IsNullOrWhiteSpace(line))
                {
                    // Append a new line for formatting
                    cleanedSql.AppendLine();
                    continue;
                }

                // Avoiding multi-line since we use AppendLine
                var tempLine = line.Replace("\n", "").Replace("\r", "");

                // Comments
                if (tempLine.StartsWith("//"))
                {
                    // Append the comment line as is
                    cleanedSql.AppendLine(tempLine);
                    continue;
                }

                string lineWithoutComment, comment;

                // Split the line into SQL code and comment (if exists)
                if (tempLine.Contains("--"))
                {
                    lineWithoutComment = tempLine[0..tempLine.IndexOf("--")].Trim();
                    comment = tempLine[tempLine.IndexOf("--")..].Replace("--", "//").Trim();
                }
                else
                {
                    lineWithoutComment = tempLine;
                    comment = string.Empty;
                }

                // Append C# code based on whether it's the first line and if it contains "SELECT"
                if (isFirst && tempLine.ToLower().Contains("select"))
                {
                    cleanedSql.Append($"cmdText.Append(\" {lineWithoutComment} \");");
                    isFirst = false;
                }
                else
                {
                    cleanedSql.Append($"cmdText.AppendLine(\" {lineWithoutComment} \");");
                }

                // Append the comment or an empty line depending on options
                if (!options.RemoveComments && !string.IsNullOrWhiteSpace(comment))
                    cleanedSql.AppendLine(comment);
                else
                    cleanedSql.AppendLine();
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in SqlToCode");

            throw;
        }

        return cleanedSql.ToString();
    }

    /// <summary>
    /// Removes unnecessary "AS" clauses from the given text.
    /// </summary>
    /// <param name="text">The input text containing SQL statements</param>
    /// <returns>The cleaned text with unnecessary "AS" clauses removed</returns>
    public static string RemoveAs(string text)
    {
        Log.Logger.Information("RemoveAs Called");

        // Split the input text into lines using commas as separators
        string[] lines = text.Split(Convert.ToChar(","));

        // Initialize a StringBuilder to store the cleaned SQL statements
        StringBuilder cleanedSql = new();

        try
        {
            foreach (string l in lines)
            {
                var line = l;

                Console.WriteLine("Before :" + line);

                bool hasComma = true;
                var index = 0;

                // Check if the line contains "fromdbo" or doesn't contain commas
                if (line.Contains("fromdbo") || !line.Contains(','))
                {
                    hasComma = false;
                    index = line.IndexOf("fromdbo");
                }

                // If "where" is present and "fromdbo" is not found, update the index
                if (line.Contains("where") && index <= 0)
                {
                    hasComma = false;
                    index = line.IndexOf("where");
                }

                // Remove "@as" and trim the line
                if (line.Contains("@as"))
                    line = line[..line.IndexOf("@as")].Trim();

                // Append "," if the line originally had a comma
                if (hasComma)
                {
                    line += ",";
                }
                // If "fromdbo" or "where" is found and "@as" is present, concatenate the remaining part
                else if (index > 0 && l.Contains("@as"))
                {
                    line += l[index..];
                }

                // Append the cleaned line to the StringBuilder
                cleanedSql.Append(line + ",");

                Console.WriteLine("After  :" + line);
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in RemoveAs");

            throw;
        }

        return cleanedSql.ToString();
    }

    /// <summary>
    /// Extracts information about database properties from the given SQL query and generates a SQL statement to retrieve metadata.
    /// This method is used by the ReadProps service to analyze a SQL query and construct a SQL statement to retrieve information about the properties.
    /// </summary>
    /// <param name="query">The input SQL query</param>
    /// <returns>A SQL statement to retrieve metadata about the properties mentioned in the query</returns>
    public static string GetPropsLengthFromQuery(string query)
    {
        Log.Logger.Information("GetPropsLengthFromQuery Called");

        // Convert the code to a SQL statement
        string sql = CodeToSql(query, new() { RemoveComments = true, CommentStyle = DevCommentType.Sql, RemoveWhiteSpace = true, IsSqlToCode = false });

        Console.WriteLine(sql);

        // Initialize data structures to store database and property information
        HashSet<string> dbs = new();
        List<SqlProp> sqlProps = new();
        bool noAs = false;

        try
        {
            // Check if "AS" clauses are used in the SQL statement
            if (sql[..sql.ToLower().IndexOf("from")].ToLower().Contains(" as "))
            {
                Console.WriteLine("======================== A ========================");
                string[] words = sql.Trim().Split(Convert.ToChar(" "));

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].ToLower() == "as")
                    {
                        Console.WriteLine("======================== A-1 ========================");
                        Console.WriteLine($"As: {words[i + 1]}");

                        string[] beforeAs;

                        // ex. WeatherConditionDesc,\nAS001.START_INVESTIGATIO AS AccidentInvestigationDate
                        if (words[i - 1].Contains('\n'))
                        {
                            beforeAs = words[i - 1][words[i - 1].LastIndexOf("\n")..].Trim().Replace("dbo.", "").Split(Convert.ToChar("."));
                        }
                        else
                        {
                            beforeAs = words[i - 1].Trim().Replace("dbo.", "").Split(Convert.ToChar("."));
                        }

                        string[] afterAs;

                        if (words[i + 1].Contains('\n'))
                        {
                            afterAs = words[i + 1][..words[i + 1].IndexOf("\n")].Trim().Split(Convert.ToChar(","));
                        }
                        else
                        {
                            afterAs = words[i + 1].Trim().Split(Convert.ToChar(","));
                        }

                        if (beforeAs.Length == 1)
                        {
                            Console.WriteLine("======================== A-1-1 ========================");
                            SqlProp sqlProp = new()
                            {
                                PropName = beforeAs[0].Replace(",", ""),
                                AsPropName = afterAs[0].Replace(",", ""),
                                //DBName = beforeAs[0].Replace(",", ""),
                                DBName = null,
                                DBFullName = beforeAs[0].Replace(",", ""),
                            };
                            sqlProps.Add(sqlProp);
                            Console.WriteLine(sqlProp.DBName + " = " + sqlProp.PropName + " as " + sqlProp.AsPropName);
                        }
                        else
                        {
                            Console.WriteLine("======================== A-1-2 ========================");
                            SqlProp sqlProp = new()
                            {
                                PropName = beforeAs[1].Replace(",", ""),
                                AsPropName = afterAs[0].Replace(",", ""),
                                DBName = beforeAs[0].Replace(",", ""),
                                DBFullName = beforeAs[0].Replace(",", ""),
                            };
                            sqlProps.Add(sqlProp);
                            Console.WriteLine(sqlProp.DBName + " - " + sqlProp.PropName + " as " + sqlProp.AsPropName);
                        }
                    }
                    else if (words[i].Contains('.'))
                    {
                        Console.WriteLine("======================== A-2 ========================");
                        string[] poropSQL = words[i].Trim().Replace("dbo", "").Split(Convert.ToChar("."));

                        if (poropSQL[0].Contains('\n'))
                        {
                            poropSQL[0] = poropSQL[0][poropSQL[0].LastIndexOf("\n")..].Trim().Replace("dbo.", "");
                        }

                        if (poropSQL[1].Contains('\n'))
                        {
                            poropSQL[1] = poropSQL[1][..poropSQL[1].IndexOf("\n")].Trim().Replace("dbo.", "");
                        }

                        Console.WriteLine($"As: {poropSQL[0]}");

                        if (poropSQL[0].Contains(' '))
                        {
                            poropSQL[0] = poropSQL[0].Trim()[poropSQL[0].IndexOf(" ")..].Trim();
                        }

                        if (poropSQL[1].IndexOf("/") != -1)
                        {
                            Console.WriteLine("======================== A-2-1 ========================");
                            SqlProp sqlProp = new()
                            {
                                PropName = poropSQL[1].Replace(",", "").Replace("-", "")[..poropSQL[1].IndexOf("/")].Trim(),
                                AsPropName = poropSQL[1].Replace(",", "").Replace("-", "")[..poropSQL[1].IndexOf("/")].Trim(),
                                DBName = poropSQL[0].Replace(",", "").Replace("-", "").Trim(),
                                DBFullName = poropSQL[0].Replace(",", "").Replace("-", "").Trim(),
                            };
                            sqlProps.Add(sqlProp);
                            Console.WriteLine($"\nDB Name: {sqlProp.DBName}, full Name: {sqlProp.DBFullName} Prop Name: {sqlProp.PropName}, As: {sqlProp.AsPropName}.\n");
                        }
                        else
                        {
                            Console.WriteLine("======================== A-2-2 ========================");
                            SqlProp sqlProp = new()
                            {
                                PropName = poropSQL[1].Replace(",", "").Replace("-", "").Trim(),
                                AsPropName = poropSQL[1].Replace(",", "").Replace("-", "").Trim(),
                                DBName = poropSQL[0].Replace(",", "").Replace("-", "").Trim(),
                                DBFullName = poropSQL[0].Replace(",", "").Replace("-", "").Trim(),
                            };
                            sqlProps.Add(sqlProp);
                            Console.WriteLine($"\nDB Name: {sqlProp.DBName}, full Name: {sqlProp.DBFullName} Prop Name: {sqlProp.PropName}, As: {sqlProp.AsPropName}.\n");
                        }
                    }

                    if (words[i].ToLower().Contains("dbo") && !words[i - 1].ToLower().Contains('='))
                    {
                        if (words[i + 1].ToLower().Contains("with"))
                        {
                            dbs.Add(words[i].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == null)
                                {
                                    sqlProps[j].DBFullName = words[i].Replace("dbo.", "");
                                }
                            }
                        }
                        else if (words[i + 2].ToLower().Contains("with"))
                        {
                            dbs.Add(words[i].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == words[i + 1])
                                {
                                    sqlProps[j].DBFullName = words[i].Replace("dbo.", "");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("======================== B ========================");
                noAs = true;
                string[] words = sql[..sql.ToLower().IndexOf("from")].Replace(",", ", - ").Split(Convert.ToChar(","));

                for (int i = 0; i < words.Length; i++)
                {
                    Console.WriteLine(words[i]);
                    if (words[i].Contains('.'))
                    {
                        string[] poropSQL = words[i].Trim().Replace("ISNULL(", "").Replace(")", "").Replace("dbo.", "").Split(Convert.ToChar("."));
                        Console.WriteLine($"As: {poropSQL[0]}");

                        // Remove any thing befor the space like 'Select'
                        if (poropSQL[0].Contains(' '))
                        {
                            poropSQL[0] = poropSQL[0].Trim()[poropSQL[0].IndexOf(" ")..].Trim();
                        }

                        // Remove any thing after the space like 'DS02.PERSONAL_NUMBER ID_NUMBER' << in some case they don't using as keyword
                        if (poropSQL[1].Contains(' '))
                        {
                            poropSQL[1] = poropSQL[1].Trim()[..poropSQL[1].IndexOf(" ")].Trim();
                        }

                        if (poropSQL[1].IndexOf("/") != -1)
                        {
                            SqlProp sqlProp = new()
                            {
                                PropName = poropSQL[1].Replace("-", "")[..poropSQL[1].IndexOf("/")].Trim(),
                                AsPropName = poropSQL[1].Replace("-", "")[..poropSQL[1].IndexOf("/")].Trim(),
                                DBName = poropSQL[0].Replace("-", "").Trim(),
                                DBFullName = poropSQL[0].Replace("-", "").Trim(),
                            };
                            sqlProps.Add(sqlProp);
                            Console.WriteLine($"\nDB Name: {sqlProp.DBName}, full Name: {sqlProp.DBFullName} Prop Name: {sqlProp.PropName}, As: {sqlProp.AsPropName}.\n");
                        }
                        else
                        {
                            SqlProp sqlProp = new()
                            {
                                PropName = poropSQL[1].Replace("-", "").Trim(),
                                AsPropName = poropSQL[1].Replace("-", "").Trim(),
                                DBName = poropSQL[0].Replace("-", "").Trim(),
                                DBFullName = poropSQL[0].Replace("-", "").Trim(),
                            };

                            sqlProps.Add(sqlProp);

                            Console.WriteLine($"\nDB Name: {sqlProp.DBName}, full Name: {sqlProp.DBFullName} Prop Name: {sqlProp.PropName}, As: {sqlProp.AsPropName}.\n");
                        }
                    }
                }
            }

            if (!sql[..sql.ToLower().IndexOf("from")].Contains(" as "))
            {
                Console.WriteLine("======================== C ========================");
                Console.WriteLine(sql.ToLower().IndexOf("from"));
                string temp = sql[sql.ToLower().IndexOf("from")..];
                string[] words = temp.Split(Convert.ToChar(" "));

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].ToLower().Contains("dbo") && !words[i - 1].ToLower().Contains('='))
                    {
                        if (words[i + 2].ToLower().Contains("with"))
                        {
                            dbs.Add(words[i].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == words[i + 1])
                                {
                                    sqlProps[j].DBFullName = words[i].Replace("dbo.", "");
                                }
                            }
                        }
                        else if (words[i + 1].ToLower().Contains("with"))
                        {
                            dbs.Add(words[i].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == words[i])
                                {
                                    sqlProps[j].DBFullName = words[i].Replace("dbo.", "");
                                }
                            }
                        }
                    }
                    else if (words[i].ToLower().Contains("with"))
                    {
                        Console.WriteLine($"\n\nHERE: {words[i]}");
                        if (words[i - 2].ToLower().Contains("join") || words[i - 2].ToLower().Contains("from"))
                        {
                            Console.WriteLine($"a1: {words[i - 1]}");
                            Console.WriteLine($"a2: {words[i - 2]}");
                            dbs.Add(words[i - 1].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == words[i - 1])
                                {
                                    sqlProps[j].DBFullName = words[i - 1].Replace("dbo.", "");
                                }
                            }
                        }
                        else if (words[i - 3].ToLower().Contains("join") || words[i - 3].ToLower().Contains("from"))
                        {
                            Console.WriteLine($"b1: {words[i - 1]}");
                            Console.WriteLine($"b2: {words[i - 2]}");
                            dbs.Add(words[i - 2].Replace("dbo.", ""));
                            for (int j = 0; j < sqlProps.Count; j++)
                            {
                                if (sqlProps[j].DBName == words[i - 1])
                                {
                                    sqlProps[j].DBFullName = words[i - 2].Replace("dbo.", "");
                                }
                            }
                        }
                    }
                }
            }

            // Create a list of database models based on the collected information
            List<DbModel> dbModels = new();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("sqlProps Count: " + sqlProps.Count);
            Console.WriteLine("dbs Count: " + dbs.Count);

            foreach (string dbName in dbs)
            {
                DbModel dbModel = new()
                {
                    DbFullName = dbName,
                };

                foreach (SqlProp prop in sqlProps)
                {
                    if (prop.DBFullName == dbModel.DbFullName)
                    {
                        dbModel.SqlProps.Add(prop);
                    }
                }

                dbModels.Add(dbModel);
            }

            // Construct the final SQL statement to retrieve metadata
            string sqlStatement;
            if (noAs)
            {
                // Construct the SQL statement when "AS" clauses are not used
                sqlStatement = $"SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE as NUMERIC_SCALE_FOR_Decimal, DATETIME_PRECISION  " +
                    "\nFROM INFORMATION_SCHEMA.COLUMNS " +
                    "\nWHERE\n";
            }
            else
            {
                // Construct the SQL statement when "AS" clauses are used
                string propsName = "\n'Prop_Name' = Case \n";

                foreach (SqlProp sqlProp in sqlProps)
                {
                    // Exclude properties where the "AS" name is the same as the original name
                    if (sqlProp.AsPropName == sqlProp.PropName)
                    {
                        continue;
                    }

                    // Add each "WHEN" clause for the "AS" names
                    propsName += $"WHEN COLUMN_NAME = '{sqlProp.PropName}' AND TABLE_NAME = '{sqlProp.DBFullName}' THEN '{sqlProp.AsPropName.Trim()}'\n";
                }

                // Complete the "END" statement for the "CASE" expression
                propsName += "END";

                // Construct the final SQL statement with "AS" clauses
                sqlStatement = $"SELECT TABLE_NAME, COLUMN_NAME, {propsName}, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE as NUMERIC_SCALE_FOR_Decimal, DATETIME_PRECISION " +
                    "\nFROM INFORMATION_SCHEMA.COLUMNS " +
                    "\nWHERE\n";
            }

            int count = 0;
            foreach (DbModel dbModel in dbModels)
            {
                Console.WriteLine("\n--------------------");
                Console.WriteLine($"DB Name {dbModel.DbFullName}\n");

                if (dbModel.SqlProps.Count < 1)
                {
                    continue;
                }

                if (count == 0)
                {
                    sqlStatement += $"( TABLE_NAME = \'{dbModel.DbFullName}\' AND (";
                }
                else
                {
                    sqlStatement += $"\nOR ( TABLE_NAME = \'{dbModel.DbFullName}\' AND (";
                }

                for (int j = 0; j < dbModel.SqlProps.Count; j++)
                {
                    Console.WriteLine($"PropName {dbModel.SqlProps[j].PropName}");
                    if (dbModel.SqlProps.Count == 1)
                    {
                        // If there is only one property, complete the WHERE clause
                        sqlStatement += $" COLUMN_NAME = \'{dbModel.SqlProps[j].PropName}\' ) )";
                    }
                    else if (j != dbModel.SqlProps.Count - 1)
                    {
                        // If it's not the last property, add "OR" to the WHERE clause
                        sqlStatement += $" COLUMN_NAME = \'{dbModel.SqlProps[j].PropName}\' OR";
                    }
                    else
                    {
                        // If it's the last property, complete the WHERE clause
                        sqlStatement += $" COLUMN_NAME = \'{dbModel.SqlProps[j].PropName}\' ) )";
                    }
                }

                count++;
            }

            // Complete the ORDER BY clause for the SQL statement
            sqlStatement += "\nORDER BY TABLE_NAME, COLUMN_NAME";

            return sqlStatement;
        }
        catch (Exception ex)
        {
            Log.Logger.Error(ex, "An unexpected error occurred in GetPropsLengthFromQuery");

            throw;
        }
    }

    #endregion Public Method

    #region Private Methods

    /// <summary>
    /// Handle comment based on the selected comment style.
    /// </summary>
    /// <param name="commentLine">Comment line</param>
    /// <param name="commentStyle">Selected comment style</param>
    /// <param name="cleanedSql">StringBuilder to store cleaned SQL</param>
    /// <returns>Comment symbol used</returns>
    private static string HandleComment(string commentLine, DevCommentType commentStyle, List<string> cleanedSql)
    {
        string commentSymbol;
        switch (commentStyle)
        {
            case DevCommentType.None:
                commentSymbol = "NONE";
                cleanedSql.Add(commentLine);
                break;

            case DevCommentType.CSharp:
                commentSymbol = "//";
                commentLine = string.Format("//{0}", commentLine[2..]);
                break;

            //case CommentType.VB:
            //    templine = string.Format("'{0}", templine.Substring(2));
            //    break;

            case DevCommentType.Sql:
                commentSymbol = "--";
                commentLine = string.Format("--{0}", commentLine[2..]);
                break;

            default:
                commentSymbol = "none";
                cleanedSql.Add(commentLine);
                break;
        }

        cleanedSql.Add(commentLine);
        return commentSymbol;
    }

    #endregion Private Methods
}