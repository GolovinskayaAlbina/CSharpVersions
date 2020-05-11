namespace CSharpVersion5.Service.Requests
{
    class GetUsersByRatingRequest
    {
        public int End { get; private set; }
        public int Start { get; private set; }
        
        public GetUsersByRatingRequest(int start, int end)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return string.Format("Start: {0}; end: {1}", Start, End);
        }
    }
}