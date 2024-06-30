using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniDrive.Helpers
{
    public class EmailTemplateHelper
    {
    public static string LoadTemplate(string templatePath)
    {
        return File.ReadAllText(templatePath);
    }

    public static string ReplacePlaceholders(string template, Dictionary<string, string> placeholders)
    {
        foreach (var placeholder in placeholders)
        {
            template = template.Replace($"{{{{{placeholder.Key}}}}}", placeholder.Value);
        }
        return template;
    }
    }
}