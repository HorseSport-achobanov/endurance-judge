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

        public ListItemViewModel(int id, string name, DelegateCommandBase action) : this(id, name, action, null)
        {
        }

        public ListItemViewModel(int id, string name, DelegateCommandBase action, DelegateCommandBase remove)
        {
            this.Id = id;
            this.Name = name;
            this.Action = action;
            this.Remove = remove;
        }

        public int Id { get; }
        public string Name { get; }
        public DelegateCommandBase Action { get; }
        public DelegateCommandBase Remove { get; }
    }
}
