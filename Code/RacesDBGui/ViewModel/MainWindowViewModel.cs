using Avalonia.Media;
using Avalonia;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Globalization;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Avalonia.Controls;
using RacesDBGui.Model;

namespace Microsoft.EntityFrameworkCore
{
    public static partial class CustomExtensions
    {
        public static IQueryable Query(this DbContext context, string entityName) =>
            context.Query(context.Model.FindEntityType(entityName).ClrType);

        static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set), Type.EmptyTypes);

        public static IQueryable Query(this DbContext context, Type entityType) =>
            (IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
    }
}
namespace RaceDBGui.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {
        public const string dbModel = "RacesDBGui.Model.";
        private List<string> _tableNames;
        private List<string> _fieldsNames;
        private ObservableCollection<Request> _requests;
        private string? _selectedTable;
        private ObservableCollection<Object> _entities;
        private ViewModelBase _content;

        public string? SelectedTable
        {
            get => _selectedTable;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTable, value);
                using (var db = new race_dbContext())
                {
                    if (_selectedTable == null)
                        return;
                    Entities = new ObservableCollection<Object>(db
                        .Query(dbModel + _selectedTable).ToDynamicList());
                }
            }
        }

        public ObservableCollection<Object> Entities
        {
            get => _entities;
            set => this.RaiseAndSetIfChanged(ref _entities, value);
        }
        
        public List<string> TableNames
        {
            get => _tableNames;
            set => this.RaiseAndSetIfChanged(ref _tableNames, value);
        }
        
        public List<string> FieldsNames
        {
            get => _fieldsNames;
            set => this.RaiseAndSetIfChanged(ref _fieldsNames, value);
        }

        public ObservableCollection<Request> Requests
        {
            get => _requests;
            set => this.RaiseAndSetIfChanged(ref _requests, value);
        }
        
        public ViewModelBase Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainWindowViewModel()
        {
            using(var db = new race_dbContext())
            {
                _tableNames = db.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .Distinct()
                    .ToList();
            }

            SelectedTable = TableNames[0];
            Content = new DataBaseViewModel();
            Requests = new ObservableCollection<Request>()
            {
                new Request("First"),
                new Request("Second")
            };
        }

        public void AddNewEntity(string name = "")
        {       
            if (name == null)
                name = _selectedTable;
            
            Type t = Type.GetType(dbModel + name); 
            Entities.Add(Activator.CreateInstance(t));         
        }
        public void RemoveSelectedEntity(int SelectedEntity)
        {   
            if (SelectedEntity >= 0)
            Entities.RemoveAt(SelectedEntity);
        }
        public void CreateRequest()
        {
            Requests.Add(new Request("New request"));
        }
        public void DeleteRequest(Request e)
        {
            Requests.Remove(e);
        }

        public void OpenRequestManager() => Content = new SQLRequestViewModel();
        
        public static string CapitalizeFirstLetter(string s)
        {
            if (String.IsNullOrEmpty(s))
                return s;
            if (s.Length == 1)
                return s.ToUpper();
            return s.Remove(1).ToUpper() + s.Substring(1);
        }
        
        public static Object ToObject(IDictionary<string, Object> source, string className)
        {
            Type t = Type.GetType(dbModel + className); 
            var someObject = Activator.CreateInstance(t);

            foreach (var item in source)
            {
                t.GetProperty(item.Key).SetValue(someObject, item.Value);
            }

            return someObject;
        }
        public static List<Dictionary<string, Object>> DynamicListFromSql(race_dbContext db, string Sql)
        {
            List<Dictionary<string, Object>> data = new List<Dictionary<string, object>>();
            using (var cmd = db.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = Sql;
                if (cmd.Connection.State != ConnectionState.Open) { cmd.Connection.Open(); }

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var row = new Dictionary<string, Object>();
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            row.Add(CapitalizeFirstLetter(dataReader.GetName(fieldCount)), dataReader[fieldCount]);
                        }
                        data.Add(row);
                    }
                }
            }

            return data;
        }
        public void ExecuteSQLQuery(Request r)
        {
            if (Entities.Count != 0)
            Entities.Clear();
            
            Content = new DataBaseViewModel();
            using (var db = new race_dbContext())
            {
                foreach (var dict in DynamicListFromSql(db, $"SELECT * FROM {r.TableName} WHERE {r.WhereCondition}"))
                {
                    Entities.Add(ToObject(dict, r.TableName));
                }
            }

        }
    }
}
