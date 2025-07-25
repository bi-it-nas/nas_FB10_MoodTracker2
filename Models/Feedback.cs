using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nas_FB10_MoodTracker2.Models;

public class Feedback
{
    public bool Enabled { get; set; }
    public string Type { get; set; } = "rating"; // default value
    public string Hint { get; set; } = string.Empty;
}