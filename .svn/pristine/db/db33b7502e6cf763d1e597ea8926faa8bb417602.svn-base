namespace Demcon.ProductionTool.Model
{
    // The order is specific, because because of the drgrading order when combining comclusions (e.g. constructing a Conclusion of a TestSequence by combining the Conclusions of mulktiple Tests).
    // A Conclusion always starts as NotTested, and can only degrade when combined. When one is Inconclusive, a combination can never result in Passed, but can result Failed.
    // When combining with this defined order, you can take the numeric values and take the minimum of those.
    public enum ETestConclusion
    {
        Failed = -1,
        Inconclusive = 0,
        NotTested = 1,
        Passed = 2,
    }
}
