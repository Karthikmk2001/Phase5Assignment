namespace PizzaPortal.Models
{
    public class Pizza
    {
        public int PizzaId { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int AvailableQuantity { get; set; }

        public string Image { get; set; }

        public int CategoryId { get; set; }

        public DateTime PizzaAddedDate { get; set; }

        public Category Category { get; set; }
      
    }
}
