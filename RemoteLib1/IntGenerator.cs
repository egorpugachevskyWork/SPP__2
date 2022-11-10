namespace RemoteLib1
{
    public class IntGenerator
    {
        public static bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

        public static int Generate(Type type)
        {
            return new Random().Next(-20, -15);
        }
    }
}