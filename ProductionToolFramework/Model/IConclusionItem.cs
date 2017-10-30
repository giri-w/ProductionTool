using System;
using Demcon.ProductionTool.General;

namespace Demcon.ProductionTool.Model
{
    public interface IConclusionItem
    {
        string Name { get; set; }
        ETestConclusion Conclusion { get; }
        event EventHandler<EventArgs<bool>> UpdatedEvent;
    }
}
