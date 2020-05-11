namespace CSharpVersion7_3.Service.Requests
{
    class GetUsersByRatingRequest
    {
        //6.0 Read-only auto-properties <code>public T Prop { get; }</code>
        public int End { get; }

        //6.0 Read-only auto-properties <code>public T Prop { get; }</code>
        public int Start { get; }

        public GetUsersByRatingRequest(int start, int end)
        {
            Start = start;
            End = end;
        }

        //6.0 String interpolation <code>$"{x}: {y}"</code>
        //7.0 More expression-bodied members smth() => "";
        public override string ToString() => $"Start: {Start}; end: {End}";
        
    }
}