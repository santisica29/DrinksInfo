using ConsoleTableExt;

namespace DrinksInfo;
internal class TableVisualisationEngine
{
    public static void ShowTable<T>(List<T> tableData, string tableName = "") where T : class
    {
        Console.Clear();

        Console.WriteLine("\n\n");

        ConsoleTableBuilder
            .From(tableData)
            .WithColumn(tableName)
            .WithFormat(ConsoleTableBuilderFormat.MarkDown)
            .ExportAndWriteLine(TableAligntment.Center);

        Console.WriteLine("\n\n");


    }
}
