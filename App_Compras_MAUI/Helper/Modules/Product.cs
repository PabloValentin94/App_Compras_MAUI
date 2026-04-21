using App_Compras_MAUI.Model;

namespace App_Compras_MAUI.Helper.Modules
{
    public class Product : SQLite
    {
        public Product() : base()
        {
            if (manager != null)
            {
                manager.CreateTableAsync<Model.Product>().Wait();
            }
        }

        public Task<int> Save(Model.Product payload)
        {
            if (manager == null)
            {
                return Task.FromResult(0);
            }

            if (payload.Id > 0)
            {
                Task<int> rows_affected = manager.UpdateAsync(payload);

                return rows_affected;
            }
            else
            {
                Task<int> rows_affected = manager.InsertAsync(payload);

                return rows_affected;
            }
        }

        public Task<int> Delete(int id)
        {
            if (manager == null)
            {
                return Task.FromResult(0);
            }

            Task<int> rows_affected = manager.Table<Model.Product>().DeleteAsync(register => register.Id == id);

            return rows_affected;
        }

        public Task<List<Model.Product>> List()
        {
            if (manager == null)
            {
                return Task.FromResult(new List<Model.Product>());
            }

            return manager.Table<Model.Product>().ToListAsync();
        }

        public Task<List<Model.Product>> Find(string search)
        {
            if (manager == null)
            {
                return Task.FromResult(new List<Model.Product>());
            }

            string sql = $"SELECT * FROM Product WHERE UPPER(Description) LIKE '%{search.ToUpper()}%'";

            return manager.QueryAsync<Model.Product>(sql);
        }
    }
}
