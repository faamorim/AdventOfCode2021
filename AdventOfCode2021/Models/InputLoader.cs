using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace AdventOfCode2021.Models
{
    internal static class InputLoader
    {
        static string DefaultInput(DayPuzzle day)
        {
            var str = day.ToString();
            switch (day)
            {
                default:
                    throw new ArgumentException("Invalid argument DayInput.");
            }
        }
        static string DefaultInput(string fileName) => $"AdventOfCode2021.Resources.DefaultInput.{fileName}";
        static List<string>? ReadResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly?.GetManifestResourceStream(resourceName);
            using StreamReader reader = new StreamReader(stream);
            string? result = reader?.ReadToEnd();
            return result?.Split(new string[] { Environment.NewLine }, StringSplitOptions.None)?.ToList();
        }

        public static List<string>? ReadDefaultInput(DayPuzzle day) => ReadResource(DefaultInput(day));

    }
}
