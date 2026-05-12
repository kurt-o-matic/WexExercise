namespace WexExercise.Data
{
    public class Sequence(long current = 0)
    {
        public long Next() => Interlocked.Increment(ref current);

        public long Last() => current;
    }
}
