using Newtonsoft.Json;
using System.Collections.Generic;

namespace SunshineAppsExporter.Models {
    internal class SunshineApp {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("cmd", NullValueHandling=NullValueHandling.Ignore)]
        public string Command { get; set; }
        [JsonProperty("prep-cmd", DefaultValueHandling=DefaultValueHandling.Ignore)]
        public IList<PrepCommand> PrepCommands { get; set; } = new List<PrepCommand>();
        [JsonProperty("detached", DefaultValueHandling=DefaultValueHandling.Ignore)]
        public IList<string> DetachedCommands { get; set; } = new List<string>();
        [JsonProperty("working-dir", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkingDir { get; set; }
        [JsonProperty("image-path", NullValueHandling=NullValueHandling.Ignore)]
        public string ImagePath { get; set; }
        [JsonProperty("output")]
        public string Output { get; set; } = string.Empty;

        public bool ShouldSerializeDetachedCommands() => DetachedCommands?.Count > 0;
        public bool ShouldSerializePrepCommands() => PrepCommands?.Count > 0;
        public bool ShouldSerializeOutput() => !string.IsNullOrWhiteSpace(Output);

        public SunshineApp WithName(string name) {
            Name = name;
            return this;
        }

        public SunshineApp WithCommand(string command) {
            Command = command;
            return this;
        }

        public SunshineApp AddPrepCommand(PrepCommand command) {
            PrepCommands.Add(command);
            return this;
        }

        public SunshineApp AppPrepCommand (string doCommand, string undoCommand) {
            PrepCommands.Add(new PrepCommand { Do = doCommand, Undo = undoCommand });
            return this;
        }
    
        public SunshineApp AddDetachedCommand(string command) {
            DetachedCommands.Add(command);
            return this;
        }

        public SunshineApp WithWorkingDir(string workingDir) {
            WorkingDir = workingDir;
            return this;
        }

        public SunshineApp WithImagePath(string imagePath) {
            ImagePath = imagePath;
            return this;
        }

        public SunshineApp WithOutput(string output) {
            Output = output;
            return this;
        }
    }

    internal class PrepCommand {
        [JsonProperty("do")]
        public string Do { get; set; }
        [JsonProperty("undo")]
        public string Undo { get; set; }
    }
}
