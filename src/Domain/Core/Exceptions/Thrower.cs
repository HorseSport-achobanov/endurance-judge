namespace EnduranceJudge.Domain.Core.Exceptions
{
    public static class Thrower
    {
        public static void Throw<TException>(string message)
            where TException : DomainObjectException, new()
        {
            var exception = new TException
            {
                DomainMessage = message
            };

            throw exception;
        }
    }
}
