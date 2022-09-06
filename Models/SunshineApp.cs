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

        public SunshineApp WithName(string name) {
            this.Name = name;
            return this;
        }

        public SunshineApp WithCommand(string command) {
            this.Command = command;
            return this;
        }

        public SunshineApp AddPrepCommand(PrepCommand command) {
            this.PrepCommands.Add(command);
            return this;
        }

        public SunshineApp AppPrepCommand (string doCommand, string undoCommand) {
            this.PrepCommands.Add(new PrepCommand { Do = doCommand, Undo = undoCommand });
            return this;
        }
    
        public SunshineApp AddDetachedCommand(string command) {
            this.DetachedCommands.Add(command);
            return this;
        }

        public SunshineApp WithWorkingDir(string workingDir) {
            this.WorkingDir = workingDir;
            return this;
        }

        public SunshineApp WithImagePath(string imagePath) {
            this.ImagePath = imagePath;
            return this;
        }

        public SunshineApp WithOutput(string output) {
            this.Output = output;
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
