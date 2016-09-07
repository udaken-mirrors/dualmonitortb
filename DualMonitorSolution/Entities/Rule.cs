using System;

namespace DualMonitor.Entities
{
    [Serializable]
    public class Rule
    {
        public const string MOVE_MONITOR_WITH_CURSOR = "Monitor with mouse cursor";

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public string Class { get; set; }

        public string Program { get; set; }

        public string Icon { get; set; }

        public bool UseCaption { get; set; }

        public bool UseProgram { get; set; }

        public bool UseClass { get; set; }

        public string Caption { get; set; }

        public string MoveAction { get; set; }

        public Guid Id { get; set; }

        internal Rule Clone()
        {
            return (Rule)MemberwiseClone();
        }
    }

    public enum RuleActionType
    {
        Move
    }
}
