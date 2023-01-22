

using System.Text;

var fødeslnummer = new Fødeslnummer();

// foreach(var nr in fødeslnummer.Generate(DateTime.Parse("1988-07-17"), Kjønn.Gutt).Reverse())
// {
//     Console.WriteLine(nr);
// }
var date =DateTime.Today.AddYears(-70);
var endDate = DateTime.Today.AddYears(-20);
long total = 0;
using (var stream = new FileStream("fnr.txt", FileMode.Create, FileAccess.Write))
while(date != endDate)
{
    date = date.AddDays(1);
    foreach(var fnr in fødeslnummer.Generate(date, Kjønn.Gutt))
    {
        total++;
        stream.Write(Encoding.ASCII.GetBytes(fnr));
        stream.WriteByte((byte)'\n');
    }
    foreach(var fnr in fødeslnummer.Generate(date, Kjønn.Jente))
    {
        total++;
        stream.Write(Encoding.ASCII.GetBytes(fnr));
        stream.WriteByte((byte)'\n');
    }
    // if(date.Day == 3)
    // {
    //     stream.Flush();
    // }
}

Console.WriteLine($"Totalt {total} fødselsnummer");