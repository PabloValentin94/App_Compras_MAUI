using SQLite;

namespace App_Compras_MAUI.Model
{
    public class Product
    {
        // Campos.

        private int _id = 0;

        private string _description = String.Empty;

        private int _quantity = 0;

        private double _price = 0;

        // Atributos.

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => this._id;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Valor inválido para o ID!");
                }

                this._id = value;
            }
        }

        public string Description
        {
            get => this._description;
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Valor inválido para a descrição!");
                }

                this._description = value.ToUpper();
            }
        }

        public int Quantity
        {
            get => this._quantity;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Valor inválido para a quantidade!");
                }

                this._quantity = value;
            }
        }

        public double Price
        {
            get => this._price;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Valor inválido para o preço!");
                }

                this._price = value;
            }
        }

        public double Total_Cost
        {
            get
            {
                return this._quantity * this._price;
            }
        }
    }
}
