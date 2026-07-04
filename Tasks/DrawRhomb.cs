public class DrawRhomb
{
    public static void Run(int N)
    {
        int centreSymmetry = N / 2 + 1;
        int border1 = centreSymmetry;
        int border2 = centreSymmetry;

        for (var i = 1; i <= centreSymmetry; i++)
        {
            string currentLine = "";

            for (var j = 1; j <= N; j++)
            {
                if (j == border1 | j == border2)
                {
                    currentLine += "X";
                }
                else
                {
                    currentLine += " ";
                }
            }

            border1 -= 1;
            border2 += 1;
            Console.WriteLine(currentLine);
        }

        border1 += 1;
        border2 -= 1;

        for (var i = centreSymmetry - 1; i > 0; i--)
        {
            string currentLine = "";
            border1 += 1;
            border2 -= 1;

            for (var j = 1; j <= N; j++)
            {
                if (j == border1 | j == border2)
                {
                    currentLine += "X";
                }
                else
                {
                    currentLine += " ";
                }
            }

            Console.WriteLine(currentLine);
        }
        
    }
 }
