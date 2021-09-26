using Prism.Commands;

namespace EnduranceJudge.Gateways.Desktop.Core.Components.Templates.ListItem
{
    public class ListItemViewModel
    {
        private ListItemViewModel()
        {
        }

        public ListItemViewModel(int id, DelegateCommandBase command) : this(id, id.ToString(), command)
        {
        }

        public ListItemViewModel(int id, string name, DelegateCommandBase command)
        {
            this.Id = id;
            this.Name = name;
            this.Command = command;
        }

        public int Id { get; }
        public string Name { get; }
        public DelegateCommandBase Command { get; }
    }
}
