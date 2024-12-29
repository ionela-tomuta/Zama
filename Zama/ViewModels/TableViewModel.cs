using Zama.Models;
using Zama.Services;

namespace Zama.ViewModels
{
    public class TableViewModel
    {
        public int Id { get; private set; }
        public int Number { get; private set; }
        public int Capacity { get; private set; }
        public string Location { get; private set; }

        public string DisplayName => $"Table {Number} ({Capacity} seats - {Location})";

        public TableViewModel(Zama.Models.Table table)
        {
            Id = table.Id;
            Number = table.Number;
            Capacity = table.Capacity;
            Location = table.Location;
        }
    }
}