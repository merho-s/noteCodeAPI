using System.Collections;

namespace noteCodeAPI.Models.Enums
{
    public class Languages : IEnumerable<Dictionary<string, List<string>>>
    {
        private Dictionary<string, List<string>> _codeTags;
        public Languages() 
        {
            _codeTags = new() {
                
                { "JavaScript", new List<string>() { "javascript", "js" } },
                { "TypeScript", new List<string>() { "typescript", "ts" } },
                { "Apex", new List<string>() { "apex" } },
                { "ASP.NET", new List<string>() { "aspnet" } },
                { "BASIC", new List<string>() { "basic" } },
                { "Batch", new List<string>() { "batch" } },
                { "C", new List<string>() { "c" } },
                { "C#", new List<string>() { "csharp", "cs" } },
                { "C++", new List<string>() { "cpp" } },
                { "CSS", new List<string>() { "css" } },
                { "CSV", new List<string>() { "csv" } },
                { "Docker", new List<string>() { "docker", "dockerfile" } },
                { "F#", new List<string>() { "fsharp" } },
                { "Dart", new List<string>() { "dart" } },
                { "Fortran", new List<string>() { "fortran" } },
                { "Git", new List<string>() { "git" } },
                { "Go", new List<string>() { "go" } },
                { "HTML", new List<string>() { "html" } },
                { "HTTP", new List<string>() { "http" } },
                { "Ini", new List<string>() { "ini" } },
                { "Java", new List<string>() { "java" } },
                { "JSON", new List<string>() { "json", "webmanifest" } },
                { "Kotlin", new List<string>() { "kotlin", "kt", "kts" } },
                { "LaTeX", new List<string>() { "latex", "tex", "context" } },
                { "Lua", new List<string>() { "lua" } },
                { "Markdown", new List<string>() { "markdown", "md" } },
                { "MATLAB", new List<string>() { "matlab" } },
                { "MongoDB", new List<string>() { "mongodb" } },
                { "nginx", new List<string>() { "nginx" } },
                { "OCaml", new List<string>() { "ocaml" } },
                { "Pascal", new List<string>() { "pascal", "objectpascal" } },
                { "PHP", new List<string>() { "php" } },
                { "PL/SQL", new List<string>() { "plsql" } },
                { "PowerShell", new List<string>() { "powershell" } },
                { "Python", new List<string>() { "python", "py" } },
                { "Q#", new List<string>() { "qsharp", "qs" } },
                { "Razor C#", new List<string>() { "cshtml", "razor" } },
                { "ReactJS", new List<string>() { "jsx" } },
                { "ReactTS", new List<string>() { "tsx" } },
                { "Regex", new List<string>() { "regex" } },
                { "Rust", new List<string>() { "rust" } },
                { "SCSS", new List<string>() { "scss" } },
                { "SQL", new List<string>() { "sql" } },
                { "VB.Net", new List<string>() { "vbnet" } },
                { "Visual Basic", new List<string>() { "visual-basic", "vb", "vba" } },
                { "Web Assembly", new List<string>() { "wasm" } },
                { "YAML", new List<string>() { "yaml", "yml" } },
                { "IDL", null },
                { "Angular", null },
                { "VueJS", null },
                { "VS Code", null },
                { "Visual Studio", null },
                { "Flutter", null },
                { "PowerApps", null },
                { "PowerAutomate", null },
                { "PowerBI", null },
                { "PowerPlatform", null },
                { "Dataverse", null },
                { "Azure", null },
                { "Ionic", null },
            };
        }

        public IEnumerator<Dictionary<string, List<string>>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
