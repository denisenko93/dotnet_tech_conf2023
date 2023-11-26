using System.Text;


Console.ReadKey();

StringBuilder sb = new StringBuilder(40000);

Console.ReadKey();

for (int i = 0; i < 500_000/360; i++)
{
    for (int j = 0; j < 360; j++)
    {
        sb.Append(Guid.NewGuid());
        sb.Append(Guid.NewGuid());
        sb.Append(Guid.NewGuid());
    }

    sb.Clear();
}

Console.ReadKey();