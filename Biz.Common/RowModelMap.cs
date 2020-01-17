using CsvHelper.Configuration;


namespace Biz.Common
{
    public class RowModelMap : ClassMap<RowModel>
    {
        public RowModelMap()
        {
            int i = 0;
            Map(m => m.Column).Index(i++);
            Map(m => m.ColumnA).Index(i++);
            Map(m => m.ColumnB).Index(i++);
            Map(m => m.ColumnC).Index(i++)
                .ConvertUsing(reader => int.TryParse(reader.GetField(3), out int r) ? r : (int?)null);
            Map(m => m.ColumnD).Index(i++)
                .ConvertUsing(reader => int.TryParse(reader.GetField(4), out int r) ? r : (int?)null);
            Map(m => m.OtherColumn).Index(i++);
        }
    }

}
