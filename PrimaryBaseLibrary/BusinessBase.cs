using System;
using System.Collections.Generic;



namespace PrimaryBaseLibrary
{
    public abstract class BusinessBase<T>:IPrimaryBase 
    {

        public string AppName{get;set;}
        public string ClassName{get;set;}

        public abstract int InsertData(T item);
        public abstract int UpdateData(T item);
        public abstract int DeleteData(int Id);
        public abstract List<T> LoadAllItemList();
        public abstract List<T> LoadItemList(T item);
        public abstract List<T> LoadItemList(T item, int startRowIndex, int maxRow);
        public abstract T LoadItem(int id);
                      
    }
}
