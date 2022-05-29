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

        private string _tableName;
        
        public string TableName
        { 
            get => _tableName; 
            set
            {
                _tableName = value;
                RaisePropertyChangedEvent("TableName");
            }
        }
        private  KeyValuePair<string, string> _joinFields;
        public  KeyValuePair<string, string> JoinFields
        { 
            get => _joinFields; 
            set
            {
                _joinFields = value;
                RaisePropertyChangedEvent("JoinFields");
            }
        }

        private  KeyValuePair<string, string> _whereFields;
        KeyValuePair<string, string> WhereFields
        { 
            get => _whereFields; 
            set
            {
                _whereFields = value;
                RaisePropertyChangedEvent("WhereFields");
            }
        }

        private string _whereCondition;
        public string WhereCondition
        { 
            get => _whereCondition; 
            set
            {
                _whereCondition = value;
                RaisePropertyChangedEvent("WhereCondition");
            }
        }
        private  string _groupField;
        public string GroupField
        { 
            get => _groupField; 
            set
            {
                _groupField = value;
                RaisePropertyChangedEvent("GroupField");
            }
        }
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
