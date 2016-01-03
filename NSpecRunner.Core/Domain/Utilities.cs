using NSpec.Domain;

namespace NSpecRunner.Core.Domain
{
    public class Utilities
    {
        public static void GenerateUniqueExampleName(Context context, ref string output, string spec=null)
        {
            output += "-" + context.Name;
            if (context.Parent != null)
                GenerateUniqueExampleName(context.Parent, ref output, spec);
            else
            {
                if(!string.IsNullOrWhiteSpace(spec))
                    output = spec + output;

                output = output.Replace(" ", "").Trim(new char[] { '-' });
            }
        }
    }
}
