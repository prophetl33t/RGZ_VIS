using Avalonia.Media;
using Avalonia;
using ReactiveUI;
using System;
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
        
        private List<string> _tableNames;
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
                        .Query("RacesDBGui.Model." + _selectedTable).ToDynamicList());
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

        public void AddNewEntity()
        {         
            Type t = Type.GetType("RacesDBGui.Model." + _selectedTable); 
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
        public void ExecuteSQLQuery()
        {
            Content = new DataBaseViewModel();
        }
    }
}
