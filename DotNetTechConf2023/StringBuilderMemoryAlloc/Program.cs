using System.Text;


Console.ReadKey();

StringBuilder sb = new StringBuilder(54000000);

Console.ReadKey();

for (int i = 0; i < 500_000; i++)
{
    sb.Append(Guid.NewGuid());
    sb.Append(Guid.NewGuid());
    sb.Append(Guid.NewGuid());
}

Console.ReadKey();

sb.Clear();

Console.ReadKey();