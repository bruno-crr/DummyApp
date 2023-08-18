using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wrappers
{
    public class ProductWrapper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int id;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; NotifyPropertyChanged(); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; NotifyPropertyChanged(); }
        }

        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; NotifyPropertyChanged(nameof(StatusDescricao)); }
        }

        private StatusType sts;

        public StatusType Sts
        {
            get { return sts; }
            set { sts = value; }
        }

        public string StatusDescricao
        {
            get
            {
                return Status == 0 ? "Deletado" : Status == 1 ? "Ativo" : "Bloqueado";
            }
        }

    }

    public enum StatusType
    {
        Deletado =0,
        Ativo = 1,
        Bloqueado = 2
    }
}
