using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace RacesDBGui.Model
{
    public class Request : INotifyPropertyChanged
    {
        private string _name;
        public string Name 
        { 
            get => _name; 
            set
            {
                _name = value;
                RaisePropertyChangedEvent("Name");
            }
        }    
        public string TableName { get; set; }
        public List<string> Fields { get; set; }
        public string Operation { get; set; }
        public string? Subrequest { get; set; }
        public Request(string name)
        {
            Name = name;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChangedEventArgs e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
