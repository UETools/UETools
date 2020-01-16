namespace UnrealTools.KismetVM.Instructions
{
    public enum EScriptInstrumentation : byte
    {
        Class = 0,
        ClassScope,
        Instance,
        Event,
        InlineEvent,
        ResumeEvent,
        PureNodeEntry,
        NodeDebugSite,
        NodeEntry,
        NodeExit,
        PushState,
        RestoreState,
        ResetState,
        SuspendState,
        PopState,
        TunnelEndOfThread,
        Stop
    }
}
