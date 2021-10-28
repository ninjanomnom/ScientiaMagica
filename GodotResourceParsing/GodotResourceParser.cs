using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GodotResourceParsing {
    public static class GodotResourceParser {
        private static Regex _definitionExtractor = new Regex(@"(?<!=\s*)\[([\[\]]+?)\]", RegexOptions.Compiled);
        
        public static async Task Parse(string filePath) {
            await Parse(File.OpenRead(filePath));
        }

        public static async Task Parse(Stream fileStream) {
            var contents = await new StreamReader(fileStream).ReadToEndAsync();
        }

        private static string PrepareContents(string contents) {
            var output = Regex.Matches(contents, @"\[([\[\]])+?\]")
        }
    }
}