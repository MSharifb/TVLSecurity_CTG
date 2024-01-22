using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace PrimaryBaseLibrary
{
    public abstract class DataAccessBase<T>:IPrimaryBase 
    {
        public string AppName { get; set; }
        public string ClassName { get; set; }       
        public abstract int Save(T item, DataSaveOption sOption);
        public abstract List<T> LoadItemList(T item, DataLoadOption lOption);
        public abstract List<T> LoadItemList(T item, DataLoadOption lOption, int startRowIndex, int maxRow);       
        public abstract T LoadItem(T item, DataLoadOption lOption);
        
        
                       
    }
}
