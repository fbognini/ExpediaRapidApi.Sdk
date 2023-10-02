using ExpediaRapidApi.Sdk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Utils
{
    public static class ExpediaReferences
    {
        private static References? references;
        private static References References => references ?? LoadReferences();
        public static List<Reference> GetCategories() => References.Categories.Values.ToList();

        private static References LoadReferences()
        {
            var text = File.ReadAllText("References/list.json");
            references = System.Text.Json.JsonSerializer.Deserialize<References>(text);

            return references!;
        }
    }
}
