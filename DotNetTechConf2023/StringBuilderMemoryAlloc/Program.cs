using System.Text;


StringBuilder sb = new StringBuilder();

for (int i = 0; i < 500_000; i++)
{
    sb.Append(Guid.NewGuid());
    sb.Append(Guid.NewGuid());
    sb.Append(Guid.NewGuid());
}

sb.Clear();